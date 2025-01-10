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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
            => _groupService = groupService;

        [HttpGet("GetGroups")]
        public IEnumerable<GroupDTO> GetGroups()
            => _groupService.GetItems().Select(group => MapperConvert<Group, GroupDTO>.ConvertItem(group)).ToList();

        [HttpGet("GetGroup/{id}")]
        [ProducesResponseType(typeof(Field), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGroup(int? id)
        {
            Group group = _groupService.GetItem(id);
            return group is null ? NotFound() : Ok(MapperConvert<Group, GroupDTO>.ConvertItem(group));
        }

        [HttpPut("UpdateGroup")]
        public IActionResult UpdateGroup(GroupDTO group)
           => _groupService.UpdateItem(MapperConvert<GroupDTO, Group>.ConvertItem(group)) ? Ok() : NotFound();

        [HttpPost("AddGroup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddGroup(GroupDTO group)
            => _groupService.AddItem(MapperConvert<GroupDTO, Group>.ConvertItem(group)) ? Ok() : NotFound();

        [HttpDelete("DeleteGroup/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteGroup(int? id)
            => _groupService.RemoveItem(_groupService.GetItem(id)) ? Ok() : NotFound();

    }
}
