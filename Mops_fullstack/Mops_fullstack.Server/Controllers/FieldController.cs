using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mops_fullstack.Server.Core.Services;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;

namespace Mops_fullstack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequireHttps]
    public class FieldController : ControllerBase
    {
        private readonly FieldService _fieldService;
        public FieldController(FieldService fieldService)
            => _fieldService = fieldService;

        /*[HttpGet("GetFields")]
        public IEnumerable<FieldDTO> GetFields()
            => _fieldService.GetItems().Select(field => MapperConvert<Field, FieldDTO>.ConvertItem(field)).ToList();

        [HttpGet("GetField/{id}")]
        [ProducesResponseType(typeof(Field), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetField(int? id)
        {
            Field field = _fieldService.GetItem(id);
            return field is null ? NotFound() : Ok(MapperConvert<Field, FieldDTO>.ConvertItem(field));
        }
        [HttpPut("UpdateField")]
        public IActionResult UpdateField(FieldDTO field)
           => _fieldService.UpdateItem(MapperConvert<FieldDTO, Field>.ConvertItem(field)) ? Ok() : NotFound();
        [HttpPost("AddField")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddField(FieldDTO field)
            => _fieldService.AddItem(MapperConvert<FieldDTO,Field>.ConvertItem(field)) ? Ok() : NotFound();
        [HttpDelete("DeleteField/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteField(int? id)
            => _fieldService.RemoveItem(_fieldService.GetItem(id)) ? Ok() : NotFound();*/


    }
}
