using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mops_fullstack.Server.Core.Services;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Mops_fullstack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequireHttps]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IThreadService _threadService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public MessageController(IMessageService messageService, IGroupService groupService, IThreadService threadService, IMapper mapper)
        {
            _messageService = messageService;
            _groupService = groupService;
            _threadService = threadService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MessageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult CreateMessage([FromBody] CreateMessageDTO message)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot create a message because no user is logged in.");
            }
            message.PlayerId = player.Id;

            Message newMessageModel = _mapper.Map<Message>(message);
            if (
                _threadService.GetItem(message.ThreadId) is not Thread thread ||
                !_groupService.IsMember(thread.GroupId, player.Id)
            )
            {
                return Unauthorized("Cannot create a message because you are not a member of that group.");
            }

            Message? newMessage = _messageService.AddItem(newMessageModel);
            if (newMessage == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<MessageDTO>(newMessage));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMessage([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot delete a message because no user is logged in.");
            }

            Message? message = _messageService.GetItem(id);
            if (message == null)
            {
                return NotFound("Cannot delete a message because it was not found.");
            }

            if (message.IsInitial)
            {
                return BadRequest("Cannot delete the initial message of a thread.");
            }

            if (!_messageService.IsOwnedBy(id, player.Id) && !_groupService.IsOwnedBy(message.Thread.GroupId, player.Id))
            {
                return Unauthorized("Cannot delete a message because you do not own it or the group.");
            }
            _messageService.RemoveItem(message);
            return NoContent();
        }

        /*[HttpGet("GetMessages")]
        public IEnumerable<MessageDTO> GetMessages()
            => _messageService.GetItems().Select(message => MapperConvert<Message, MessageDTO>.ConvertItem(message)).ToList();

        [HttpGet("GetMessage/{id}")]
        [ProducesResponseType(typeof(Field), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMessages(int? id)
        {
            Message message = _messageService.GetItem(id);
            return message is null ? NotFound() : Ok(MapperConvert<Message, MessageDTO>.ConvertItem(message));
        }

        [HttpPut("UpdateMessage")]
        public IActionResult UpdateMessage(MessageDTO message)
           => _messageService.UpdateItem(MapperConvert<MessageDTO, Message>.ConvertItem(message)) ? Ok() : NotFound();

        [HttpPost("AddMessage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddMessage(MessageDTO message)
            => _messageService.AddItem(MapperConvert<MessageDTO, Message>.ConvertItem(message)) ? Ok() : NotFound();

        [HttpDelete("DeleteMessage/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMessage(int? id)
            => _messageService.RemoveItem(_messageService.GetItem(id)) ? Ok() : NotFound();*/
    }
}
