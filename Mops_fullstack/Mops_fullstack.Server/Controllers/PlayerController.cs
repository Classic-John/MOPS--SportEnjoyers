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
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;
        public PlayerController(PlayerService playerService)
            => _playerService = playerService;

        [HttpGet("GetPlayers")]
        public IEnumerable<PlayerDTO> GetPlayers()
            => _playerService.GetItems().Select(player => MapperConvert<Player, PlayerDTO>.ConvertItem(player)).ToList();

        [HttpGet("GetPlayer/{id}")]
        [ProducesResponseType(typeof(Field), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPlayer(int? id)
        {
            Player player = _playerService.GetItem(id);
            return player is null ? NotFound() : Ok(MapperConvert<Player, PlayerDTO>.ConvertItem(player));
        }

        [HttpPut("UpdatePlayer")]
        public IActionResult UpdatePlayer(PlayerDTO player)
           => _playerService.UpdateItem(MapperConvert<PlayerDTO, Player>.ConvertItem(player)) ? Ok() : NotFound();

        [HttpPost("AddPlayer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddPlayer(PlayerDTO player)
            => _playerService.AddItem(MapperConvert<PlayerDTO, Player>.ConvertItem(player)) ? Ok() : NotFound();

        [HttpDelete("DeletePlayer/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePlayer(int? id)
            => _playerService.RemoveItem(_playerService.GetItem(id)) ? Ok() : NotFound();
    }
}
