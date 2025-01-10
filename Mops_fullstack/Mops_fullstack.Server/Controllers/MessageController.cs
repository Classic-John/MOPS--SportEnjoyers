using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mops_fullstack.Server.Core.Services;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;
        public MessageController(MessageService messageService)
            => _messageService = messageService;

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
