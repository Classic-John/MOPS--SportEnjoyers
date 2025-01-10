using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult AddGroup([FromBody] CreateGroupDTO group)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot create a new group because the user is not authenticated.");
            }

            group.OwnerId = player.Id;
            group.Players.Add(player);
            return _groupService.AddItem(_mapper.Map<Group>(group)) ? NoContent() : BadRequest();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GroupDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGroup([FromRoute] int id)
        {
            Group? group = _groupService.GetGroupData(id);
            return group is null ? NotFound() : Ok(_mapper.Map<GroupDTO>(group));
        }

        /*[HttpPut("UpdateGroup")]
        public IActionResult UpdateGroup(GroupDTO group)
           => _groupService.UpdateItem(MapperConvert<GroupDTO, Group>.ConvertItem(group)) ? Ok() : NotFound();

        [HttpDelete("DeleteGroup/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteGroup(int? id)
            => _groupService.RemoveItem(_groupService.GetItem(id)) ? Ok() : NotFound();*/

    }
}
