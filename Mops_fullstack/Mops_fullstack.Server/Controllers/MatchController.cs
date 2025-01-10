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
    public class MatchController : ControllerBase
    {
        private readonly MatchService _matchService;
        public MatchController(MatchService matchService)
           => _matchService = matchService;

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
