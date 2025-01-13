using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Mops_fullstack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequireHttps]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<GroupSearchDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetGroups([FromQuery] GroupFilterDTO filter)
        {
            if (filter.Yours)
            {
                if (HttpContext.Items["Player"] is not Player player)
                {
                    return Unauthorized("Cannot get groups player belongs in because the user could not be found.");
                }
                filter.PlayerId = player.Id;
            }
            return Ok(_groupService.GetItemsThatMatch(filter).Select(_mapper.Map<GroupSearchDTO>).ToList());
        }

        [HttpPost]
        [ProducesResponseType(typeof(GroupDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult AddGroup([FromBody] CreateGroupDTO group)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot create a new group because the user is not authenticated.");
            }

            group.OwnerId = player.Id;
            Group? new_group = _groupService.AddItem(_mapper.Map<Group>(group));
            return new_group != null ? Ok(_mapper.Map<GroupDTO>(new_group)) : BadRequest();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GroupDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGroup([FromRoute] int id)
        {
            Player? player = HttpContext.Items["Player"] as Player;
            Group? group = _groupService.GetGroupData(id);
            if (group == null)
            {
                return NotFound();
            }

            GroupDTO groupDTO = _mapper.Map<GroupDTO>(group);
            groupDTO.IsYours = player is not null && group.OwnerId == player.Id;
            return Ok(groupDTO);
        }

        [HttpGet("{id}/requests")]
        [ProducesResponseType(typeof(GroupJoinStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetJoinStatus([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Ok(new GroupJoinStatus { Status = GroupJoinStatusTypes.NoRequest });
            }

            if (_groupService.GetJoinStatus(id, player.Id) is not GroupJoinStatus status)
            {
                return BadRequest();
            }

            return Ok(status);
        }

        [HttpPost("{id}/requests")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult AddJoinRequest([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot send a group join request because no user is logged in.");
            }

            if (!_groupService.AddJoinRequest(id, player))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut("{id}/requests")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult AddJoinVerdict([FromRoute] int id, [FromBody] JoinRequestVerdict verdict)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot send a group join verdict because no user is logged in.");
            }

            if (!_groupService.AddJoinVerdict(id, player.Id, verdict))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}/requests")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult LeaveGroup([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot send a group leave request because no user is logged in.");
            }

            if (!_groupService.RemoveFromGroup(id, player.Id))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}/requests/{playerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult KickFromGroup([FromRoute] int id, [FromRoute] int playerId)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot kick a player from a group because no user is logged in.");
            }
            if (!_groupService.IsOwnedBy(id, player.Id))
            {
                return Unauthorized("Cannot kick a player from a group because you don't own the group.");
            }

            if (!_groupService.RemoveFromGroup(id, playerId))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult DeleteGroup([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot delete a group because no user is logged in.");
            }
            if (!_groupService.IsOwnedBy(id, player.Id))
            {
                return Unauthorized("Cannot delete a group because you don't own the group.");
            }

            if (!_groupService.DeleteGroup(id))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("{id}/matches")]
        [ProducesResponseType(typeof(ICollection<MatchDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMatches([FromRoute] int id)
        {
            ICollection<Match>? matches = _groupService.GetMatches(id);
            if (matches == null)
            {
                return BadRequest("Could not retrieve matches because there is no group found with the given id.");
            }
            return Ok(matches.Select(_mapper.Map<MatchDTO>));
        }

        [HttpGet("{id}/threads")]
        [ProducesResponseType(typeof(ICollection<ThreadSummaryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetThreads([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot get threads of a group because no user is logged in.");
            }
            if (!_groupService.IsMember(id, player.Id))
            {
                return Unauthorized("Cannot get threads of a group because you are not a member.");
            }

            ICollection<Thread>? threads = _groupService.GetThreads(id);
            if (threads == null)
            {
                return NotFound("Cannot get threads of a not found group.");
            }
            return Ok(threads.OrderByDescending(thread => thread.Messages.MaxBy(message => message.DateCreated)!.DateCreated).Select(thread =>
            {
                ThreadSummaryDTO threadDTO = _mapper.Map<ThreadSummaryDTO>(thread);
                threadDTO.InitialMessage = _mapper.Map<MessageDTO>(thread.Messages.First(message => message.IsInitial));
                return threadDTO;
            }));
        }
    }
}
