using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mops_fullstack.Server.Core.Services;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Jwt;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Mops_fullstack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IJwtUtils jwtUtils, IMapper mapper)
        {
            _playerService = playerService;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<PlayerDTO>), StatusCodes.Status200OK)]
        [Authorize, ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IEnumerable<PlayerDTO> GetPlayers()
            => _playerService.GetItems().Select(_mapper.Map<PlayerDTO>).ToList();

        [HttpGet("{PlayerId}")]
        [ProducesResponseType(typeof(PlayerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPlayer([FromRoute]int playerId)
        {
            Player player = _playerService.GetItem(playerId);
            return player is null ? NotFound() : Ok(_mapper.Map<PlayerDTO>(player));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterPlayer([FromBody]PlayerRegisterDTO player)
        {
            player.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(player.Password);
            return
                _playerService.AddItem(_mapper.Map<Player>(player))
                    ? Ok()
                    : BadRequest("Failed to add player to database.");
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoggedPlayerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult LoginPlayer([FromBody]PlayerLoginDTO PlayerDTO)
        {
            if (_playerService.GetPlayerWithEmail(PlayerDTO.Email) is not Player Player)
            {
                return NotFound("No user with given credentials found.");
            }

            if (!BCrypt.Net.BCrypt.EnhancedVerify(PlayerDTO.Password, Player.Password))
            {
                return NotFound("No user with given credentials found.");
            }

            string token = _jwtUtils.GenerateJwtToken(Player);
            return Ok(new LoggedPlayerDTO(Player, token));
        }

        [HttpGet("Group/{groupId}/Join")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CheckIsMember([FromRoute]int groupId)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot check if the user is a member of the group because no user is logged in.");
            }

            if (_playerService.IsMemberOf(player.Id, groupId))
            {
                return NoContent();
            }

            if (_playerService.IsPendingRequest(player.Id, groupId))
            {
                return Forbid();
            }

            return NotFound("The player is not part of that group.");
        }

        [HttpPost("Group/{groupId}/Join")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SendJoinRequest([FromRoute]int groupId)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot send a group join request because no user is logged in.");
            }

            if (!_playerService.SendJoinRequest(player.Id, groupId))
            {
                return NotFound("Unable to send a group join request.");
            }

            return NoContent();
        }

        [HttpPut("Group/{groupId}/Join/{playerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResolveJoinRequest([FromRoute]int groupId, [FromRoute]int playerId, [FromBody]JoinRequestVerdict verdict)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot set a group join verdict because no user is logged in.");
            }

            if (!_playerService.IsOwnerOf(player.Id, groupId))
            {
                return Unauthorized("Cannot resolve a join request for a group you do not own.");
            }

            if (!_playerService.ResolveJoinRequest(playerId, groupId, verdict))
            {
                return BadRequest("Cannot resolve the join request; input data is not correct.");
            }

            return NoContent();
        }

        /*[HttpPut("UpdatePlayer")]
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
            => _playerService.RemoveItem(_playerService.GetItem(id)) ? Ok() : NotFound();*/
    }
}
