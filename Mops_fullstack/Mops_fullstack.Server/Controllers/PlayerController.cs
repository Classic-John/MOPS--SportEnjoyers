﻿using AutoMapper;
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
                _playerService.AddItem(_mapper.Map<Player>(player)) != null
                    ? Created()
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

        [HttpGet("requests")]
        [ProducesResponseType(typeof(ICollection<JoinRequestDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult GetJoinRequests()
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot send a group join request because no user is logged in.");
            }

            ICollection<JoinRequestDTO> joinRequests = [];
            player = _playerService.GetWithJoinRequests(player.Id);
            foreach (Group group in player.GroupsOwned)
            {
                GroupDTO groupDTO = _mapper.Map<GroupDTO>(group);

                foreach (Player gplayer in group.PlayerRequests)
                {
                    joinRequests.Add(new JoinRequestDTO(_mapper.Map<PlayerDTO>(gplayer), groupDTO));
                }
            }

            return Ok(joinRequests);
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
