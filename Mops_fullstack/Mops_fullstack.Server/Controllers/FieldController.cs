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
    [RequireHttps]
    public class FieldController : ControllerBase
    {
        private readonly IFieldService _fieldService;
        private readonly IMapper _mapper;
        public FieldController(IFieldService fieldService, IMapper mapper)
        {
            _fieldService = fieldService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<FieldSearchDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetFields([FromQuery] FieldFilterDTO filter)
        {
            if (filter.FreeOnDay != null && !_fieldService.IsValidDate(filter.FreeOnDay))
            {
                return BadRequest("Wrong date format.");
            }
            if (filter.Yours)
            {
                if (HttpContext.Items["Player"] is not Player player)
                {
                    return Unauthorized("Cannot get fields player belongs in because the user could not be found.");
                }
                filter.OwnerId = player.Id;
            }
            return Ok(_fieldService.GetItemsThatMatch(filter).Select(_mapper.Map<FieldSearchDTO>).ToList());
        }

        [HttpPost]
        [ProducesResponseType(typeof(FieldDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult CreateField([FromBody] CreateFieldDTO field)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot create a new field because the user is not authenticated.");
            }

            field.OwnerId = player.Id;
            Field? new_field = _fieldService.AddItem(_mapper.Map<Field>(field));
            return new_field != null ? Ok(_mapper.Map<FieldDTO>(new_field)) : BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult DeleteField([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot delete a field because the user is not authenticated.");
            }
            if (!_fieldService.IsOwnedBy(id, player.Id))
            {
                return Unauthorized("Cannot delete a field because you do not own it.");
            }

            if (!_fieldService.DeleteField(id))
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetField([FromRoute] int id)
        {

            Field? field = _fieldService.GetItem(id);
            if (field == null)
            {
                return NotFound("The requested field was not found.");
            }

            FieldDTO fieldDTO = _mapper.Map<FieldDTO>(field);
            fieldDTO.ReservedDates = field.Matches.Select(match => match.MatchDate).ToList();
            if (HttpContext.Items["Player"] is Player player)
            {
                fieldDTO.IsYours = (player.Id == field.OwnerId);
            }
            else
            {
                fieldDTO.IsYours = false;
            }

            return Ok(fieldDTO);
        }

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
