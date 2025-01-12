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
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public MatchController(IMatchService matchService, IGroupService groupService, IMapper mapper)
        {
            _matchService = matchService;
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MatchDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult AddMatch([FromBody] CreateMatchDTO match)
        {
            if (!_matchService.IsValidDate(match.MatchDate))
            {
                return BadRequest("Bad date format. Must be YYYY/MM/DD.");
            }
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot create a new match because the user is not authenticated.");
            }
            if (_matchService.AlreadyReserved(match.FieldId, match.MatchDate))
            {
                return Unauthorized("Cannot create a new match because the field is already reserved at that date.");
            }
            if (!_groupService.IsOwnedBy(match.GroupId, player.Id))
            {
                return Unauthorized("Cannot create a new match because you do not own that group.");
            }

            Match? newMatch = _matchService.AddItem(_mapper.Map<Match>(match));
            if (newMatch == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<MatchDTO>(newMatch));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMatch([FromRoute] int id)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot delete the because the user is not authenticated.");
            }

            Match? match = _matchService.GetOwnedBy(id, player.Id);
            if (match == null)
            {
                return NotFound("Could not find the requested match owned by the logged in player.");
            }
            _matchService.RemoveItem(match);
            return NoContent();
        }

        /*[HttpGet("GetMatches")]
        public IEnumerable<MatchDTO> GetMatches()
            => _matchService.GetItems().Select(match => MapperConvert<Match, MatchDTO>.ConvertItem(match)).ToList();

        [HttpGet("GetMatch/{id}")]
        [ProducesResponseType(typeof(Field), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMatch(int? id)
        {
            Match match = _matchService.GetItem(id);
            return match is null ? NotFound() : Ok(MapperConvert<Match, MatchDTO>.ConvertItem(match));
        }

        [HttpPut("UpdateMatch")]
        public IActionResult UpdateMatch(MatchDTO match)
           => _matchService.UpdateItem(MapperConvert<MatchDTO, Match>.ConvertItem(match)) ? Ok() : NotFound();

        [HttpPost("AddMatch")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddMatch(MatchDTO match)
            => _matchService.AddItem(MapperConvert<MatchDTO, Match>.ConvertItem(match)) ? Ok() : NotFound();

        [HttpDelete("DeleteMatch/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMatch(int? id)
            => _matchService.RemoveItem(_matchService.GetItem(id)) ? Ok() : NotFound();*/

    }
}
