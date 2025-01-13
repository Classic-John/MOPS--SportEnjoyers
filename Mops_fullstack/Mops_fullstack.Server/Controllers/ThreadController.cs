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
    public class ThreadController : ControllerBase
    {
        private readonly IThreadService _threadService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public ThreadController(IThreadService threadService, IGroupService groupService, IMapper mapper)
        {
            _threadService = threadService;
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ThreadDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetThread([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot get a thread because no user is logged in.");
            }

            Thread? thread = _threadService.GetWithMessages(id);
            if (thread == null)
            {
                return NotFound("Cannot get a thread because it was not found.");
            }
            if (!_groupService.IsMember(thread.GroupId, player.Id))
            {
                return Unauthorized("Cannot get a thread for a group you are not a member of.");
            }

            return Ok(_mapper.Map<ThreadDTO>(thread));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ThreadSummaryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult CreateThread([FromBody] CreateThreadDTO thread)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot create a thread because no user is logged in.");
            }
            if (!_groupService.IsMember(thread.GroupId, player.Id))
            {
                return Unauthorized("Cannot create a thread for a group you are not a member of.");
            }

            Thread newThreadModel = _mapper.Map<Thread>(thread);
            Message newMessageModel = _mapper.Map<Message>(new CreateMessageDTO(thread.InitialMessage, player.Id, newThreadModel.Id));
            newThreadModel.Messages.Add(newMessageModel);
            newMessageModel.IsInitial = true;

            Thread? newThread = _threadService.AddItem(newThreadModel);
            if (newThread == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<ThreadSummaryDTO>(newThread));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteThread([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot delete a thread because no user is logged in.");
            }

            Thread? thread = _threadService.GetWithMessages(id);
            if (thread == null)
            {
                return NotFound("Cannot delete a thread because it was not found.");
            }
            if (!_groupService.IsOwnedBy(thread.GroupId, player.Id))
            {
                return Unauthorized("Cannot delete a thread because you do not own the group.");
            }

            _threadService.RemoveItem(thread);
            return NoContent();
        }

        /*[HttpGet("GetThreads")]
        public IEnumerable<ThreadDTO> GetThreads()
            => _threadService.GetItems().Select(player => MapperConvert<Thread, ThreadDTO>.ConvertItem(player)).ToList();

        [HttpGet("GetThread/{id}")]
        [ProducesResponseType(typeof(Field), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetThread(int? id)
        {
            Thread thread = _threadService.GetItem(id);
            return thread is null ? NotFound() : Ok(MapperConvert<Thread, ThreadDTO>.ConvertItem(thread));
        }

        [HttpPut("UpdateThread")]
        public IActionResult UpdateThread(ThreadDTO thread)
           => _threadService.UpdateItem(MapperConvert<ThreadDTO, Thread>.ConvertItem(thread)) ? Ok() : NotFound();

        [HttpPost("AddThread")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddThread(ThreadDTO thread)
            => _threadService.AddItem(MapperConvert<ThreadDTO, Thread>.ConvertItem(thread)) ? Ok() : NotFound();

        [HttpDelete("DeleteThread/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteThread(int? id)
            => _threadService.RemoveItem(_threadService.GetItem(id)) ? Ok() : NotFound();*/
    }
}
