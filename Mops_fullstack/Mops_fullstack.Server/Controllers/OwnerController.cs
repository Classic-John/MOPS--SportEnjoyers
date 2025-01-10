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
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        => _ownerService = ownerService;

        [HttpGet("GetOwners")]
        public IEnumerable<OwnerDTO> GetOwners()
            => _ownerService.GetItems().Select(message => MapperConvert<Owner, OwnerDTO>.ConvertItem(message)).ToList();

        [HttpGet("GetOwner/{id}")]
        [ProducesResponseType(typeof(Field), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOwner(int? id)
        {
            Owner owner = _ownerService.GetItem(id);
            return owner is null ? NotFound() : Ok(MapperConvert<Owner, OwnerDTO>.ConvertItem(owner));
        }

        [HttpPut("UpdateOwner")]
        public IActionResult UpdateMessage(OwnerDTO owner)
           => _ownerService.UpdateItem(MapperConvert<OwnerDTO, Owner>.ConvertItem(owner)) ? Ok() : NotFound();

        [HttpPost("AddOwner")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddOwner(OwnerDTO owner)
            => _ownerService.AddItem(MapperConvert<OwnerDTO, Owner>.ConvertItem(owner)) ? Ok() : NotFound();

        [HttpDelete("DeleteOwner/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteOwner(int? id)
            => _ownerService.RemoveItem(_ownerService.GetItem(id)) ? Ok() : NotFound();
    }
}
