using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mops_fullstack.Server.Core.Mail;
using Mops_fullstack.Server.Datalayer.DTOs;
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
        private readonly IGroupService _groupService;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMailUtil _mailUtil;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IGroupService groupService, IJwtUtils jwtUtils, IMailUtil mailUtil, IMapper mapper)
        {
            _playerService = playerService;
            _groupService = groupService;
            _jwtUtils = jwtUtils;
            _mailUtil = mailUtil;
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
        public IActionResult RegisterPlayer([FromBody] PlayerRegisterDTO player)
        {
            player.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(player.Password);
            Player newPlayer = _mapper.Map<Player>(player);
            newPlayer.VerificationCode = Guid.NewGuid().ToString();

            if (_playerService.AddItem(newPlayer) != null)
            {
                _mailUtil.SendVerificationMail(newPlayer);
                return Created();
            }
            else
            {
                return BadRequest("Failed to add player to database.");
            }
        }

        [HttpPut("verify")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult VerifyEmail([FromBody] VerifyEmailDTO verification)
        {
            if (!Guid.TryParse(verification.VerificationCode, out _))
            {
                return BadRequest("Bad verification code format.");
            }
            if (_playerService.GetUnverified(verification.VerificationCode) is not Player player)
            {
                return NotFound("No unverified player with that verification code was found.");
            }

            player.Verified = true;
            if (!_playerService.UpdateItem(player))
            {
                return BadRequest();
            }
            return NoContent();
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

        [HttpPut]
        [ProducesResponseType(typeof(LoggedPlayerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult UpdatePlayer([FromBody] UpdatePlayerDTO updatePlayer)
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot update player data because no user is logged in.");
            }

            if (updatePlayer.OldPassword is null && updatePlayer.Password is not null)
            {
                return BadRequest("Cannot update player password because old password was not provided.");
            }

            if (updatePlayer.Password is not null)
            {
                if (!BCrypt.Net.BCrypt.EnhancedVerify(updatePlayer.OldPassword, player.Password))
                {
                    return Unauthorized("Cannot update player password because old password is incorrect.");
                }
                player.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(updatePlayer.Password);
            }
            if (updatePlayer.Name is not null)
            {
                player.Name = updatePlayer.Name;
            }
            if (updatePlayer.Age is int age)
            {
                player.Age = age;
            }

            if (!_playerService.UpdateItem(player))
            {
                return BadRequest("Update player failed.");
            }
            string token = _jwtUtils.GenerateJwtToken(player);
            return Ok(new LoggedPlayerDTO(player, token));
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

        [HttpGet("groups")]
        [ProducesResponseType(typeof(ICollection<GroupSearchDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        public IActionResult GetGroupsOwned()
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot get groups owned because no user is logged in.");
            }

            return Ok(_playerService.GetOwnedGroups(player.Id).Select(group => _mapper.Map<GroupSearchDTO>(group)));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized), Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePlayer()
        {
            if (HttpContext.Items["Player"] is not Player player)
            {
                return Unauthorized("Cannot delete account because no user is logged in.");
            }

            ICollection<Group> groups = _playerService.GetOwnedGroups(player.Id);
            foreach(Group group in groups.ToArray())
            {
                if (!_groupService.RemoveItem(group))
                {
                    return BadRequest("Cannot delete player account because could not delete owned groups.");
                }
            }

            if (!_playerService.RemoveItem(player))
            {
                return NotFound("Cannot delete player account because it was not found.");
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
