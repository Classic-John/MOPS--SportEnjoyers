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
    public class ThreadController : ControllerBase
    {
        private readonly ThreadService _threadService;
        public ThreadController(ThreadService threadService)
            => _threadService = threadService;
        [HttpGet("GetThreads")]
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
            => _threadService.RemoveItem(_threadService.GetItem(id)) ? Ok() : NotFound();
    }
}
