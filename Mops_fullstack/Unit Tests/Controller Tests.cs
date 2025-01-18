using System.Drawing;
using System.Numerics;
using System.Text.RegularExpressions;
using AutoMapper;
using Azure;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Mops_fullstack.Server.Controllers;
using Mops_fullstack.Server.Core;
using Mops_fullstack.Server.Core.Mail;
using Mops_fullstack.Server.Core.Services;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Interfaces;
using Mops_fullstack.Server.Datalayer.Jwt;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;
using Field = Mops_fullstack.Server.Datalayer.Models.Field;
using Group = Mops_fullstack.Server.Datalayer.Models.Group;
using Match = Mops_fullstack.Server.Datalayer.Models.Match;
using Message = Mops_fullstack.Server.Datalayer.Models.Message;
using Thread = Mops_fullstack.Server.Datalayer.Models.Thread;

namespace Unit_Tests
{
    public class ControllerTests
    {
        #region Attributes
        private IFieldService _fieldService;
        private IGroupService _groupService;
        private IMatchService _matchService;
        private IMessageService _messageService;
        private IOwnerService _ownerService;
        private IPlayerService _playerService;
        private IThreadService _threadService;
        private IMapper _mapper;
        private IJwtUtils _jwtUtils;
        private IMailUtil _mailUtil;
        private FieldController _fieldController;
        private GroupController _groupController;
        private MatchController _matchController;
        private MessageController _messageController;
        private PlayerController _playerController;
        private ThreadController _threadController;
        #endregion
        public ControllerTests()
        {
            _fieldService = A.Fake<IFieldService>();
            _groupService = A.Fake<IGroupService>();
            _matchService = A.Fake<IMatchService>();
            _messageService = A.Fake<IMessageService>();
            _playerService = A.Fake<IPlayerService>();
            _threadService = A.Fake<IThreadService>();
            _mapper = A.Fake<IMapper>();
            _jwtUtils = A.Fake<IJwtUtils>();
            _mailUtil = A.Fake<IMailUtil>();
            _fieldController = new(_fieldService, _mapper);
            _groupController = new(_groupService, _mapper);
            _matchController = new(_matchService, _groupService, _mapper);
            _messageController = new(_messageService, _groupService, _threadService, _mapper);
            _playerController = new(_playerService, _groupService, _jwtUtils, _mailUtil, _mapper);
            _threadController = new(_threadService, _groupService, _mapper);

            var httpContext = A.Fake<HttpContext>();
            var httpResponse = A.Fake<HttpResponse>();

            A.CallTo(() => httpContext.Response).Returns(httpResponse);

            var context = new ControllerContext(new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor()));
            _fieldController.ControllerContext = context;
            _groupController.ControllerContext = context;
            _matchController.ControllerContext = context;
            _messageController.ControllerContext = context;
            _playerController.ControllerContext = context;
            _threadController.ControllerContext = context;
        }
        #region FieldController
        [Fact]
        public void FieldController_GetFields_ReturnsSuccess()
        {
            #region Arrange
            var fields = A.Fake<IEnumerable<Field>>();
            A.CallTo(() => _fieldService.GetItems()).Returns(fields.ToList());
            #endregion

            #region Act
            var result = _fieldController.GetFields(new FieldFilterDTO());
            var okResult = result as OkObjectResult;
            #endregion

            #region Assert
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            okResult!.Value.Should().BeOfType(typeof(List<FieldSearchDTO>));
            #endregion

        }
        [Fact]
        public void FieldController_GetFields_ReturnsFailure()
        {
            #region Arrange
            var fields = A.Fake<IEnumerable<Field>>();
            A.CallTo(() => _fieldService.GetItems()).Returns(fields.ToList());
            #endregion

            #region Act
            var result = _fieldController.GetFields(new FieldFilterDTO("2378623"));
            var failedResult = result as BadRequestObjectResult;
            #endregion

            #region Assert
            failedResult.Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void FieldController_GetFields_ReturnsEmpty()
        {
            #region Arrange
            A.CallTo(() => _fieldService.GetItems());
            #endregion

            #region Act
            var result = _fieldController.GetFields(new FieldFilterDTO());
            var okResult = result as OkObjectResult;
            #endregion

            #region Assert
            okResult.Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void FieldController_GetField_ReturnsOk()
        {
            #region Arrange
            _fieldController.ControllerContext.HttpContext.Items["Player"] = A.Fake<Player>();
            int id = 1;
            var field = A.Fake<Field>();
            A.CallTo(() => _fieldService.GetItem(id)).Returns(field);
            #endregion
            #region Act
            var result = _fieldController.GetField(id);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            #endregion
        }

        [Fact]
        public void FieldController_GetField_ReturnsNotFound()
        {
            #region Arrange
            _fieldController.ControllerContext.HttpContext.Items["Player"] = A.Fake<Player>();
            Field? field = null;
            A.CallTo(() => _fieldService.GetItem(-1)).Returns(field);
            #endregion
            #region Act
            var result = _fieldController.GetField(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            #endregion
        }
        [Fact]
        public void FieldController_AddField_ReturnsOk()
        {
            #region Arrange
            _fieldController.ControllerContext.HttpContext.Items["Player"] = A.Fake<Player>();
            var createField = A.Fake<CreateFieldDTO>();
            var field = _mapper.Map<Field>(createField);
            var fieldDTO = _mapper.Map<FieldDTO>(field);
            A.CallTo(() => _fieldService.AddItem(field)).Returns(field);
            #endregion
            #region Act
            var result = _fieldController.CreateField(createField);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (result as OkObjectResult)!.Value.Should().BeEquivalentTo(fieldDTO);
            #endregion
        }
        [Fact] //*
        public void FieldController_DeleteField_ReturnsOk()
        {
            #region Arrange
            Player player = A.Fake<Player>();
            _fieldController.ControllerContext.HttpContext.Items["Player"] = player;
            var field = A.Fake<Field>();
            A.CallTo(() => _fieldService.RemoveItem(field)).Returns(true);
            A.CallTo(() => _fieldService.IsOwnedBy(field.Id, player.Id)).Returns(true);
            A.CallTo(() => _fieldService.DeleteField(field.Id)).Returns(true);
            #endregion
            #region Act
            var result = _fieldController.DeleteField(field.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NoContentResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
        #region GroupController
        [Fact]
        public void GroupController_GetGroups_ReturnsSuccess()
        {
            #region Arrange
            var groups = A.Fake<IEnumerable<Group>>();
            A.CallTo(() => _groupService.GetItems()).Returns(groups.ToList());
            #endregion

            #region Act
            var expectedGroups = _groupController.GetGroups(new GroupFilterDTO());
            #endregion

            #region Assert
            expectedGroups.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (expectedGroups as OkObjectResult)!.Value.Should().BeOfType<List<GroupSearchDTO>>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void GroupController_GetGroups_ReturnsEmpty()
        {
            #region Arrange
            A.CallTo(() => _groupService.GetItems()).Returns(new List<Group>());
            #endregion

            #region Act
            var expectedGroups = _groupController.GetGroups(new GroupFilterDTO());
            #endregion

            #region Assert
            expectedGroups.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (expectedGroups as OkObjectResult)!.Value.Should().BeEquivalentTo(new List<GroupSearchDTO>());
            #endregion
        }
        [Fact]
        public void GroupController_GetGroup_ReturnsOk()
        {
            #region Arrange
            _fieldController.ControllerContext.HttpContext.Items["Player"] = null;
            int id = 1;
            var field = A.Fake<Group>();
            A.CallTo(() => _groupService.GetItem(id)).Returns(field);
            #endregion
            #region Act
            var result = _groupController.GetGroup(id);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            #endregion
        }

        [Fact]
        public void GroupController_GetGroup_ReturnsNotFound()
        {
            #region Arrange
            _fieldController.ControllerContext.HttpContext.Items["Player"] = null;
            A.CallTo(() => _groupService.GetGroupData(-1)).Returns(null);
            #endregion
            #region Act
            var result = _groupController.GetGroup(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>();
            #endregion
        }
        [Fact] //*
        public void GroupController_AddGroup_ReturnsOk()
        {
            #region Arrange
            _fieldController.ControllerContext.HttpContext.Items["Player"] = A.Fake<Player>();
            var createGroup = A.Fake<CreateGroupDTO>();
            var group = _mapper.Map<Group>(createGroup);
            var groupDTO = _mapper.Map<GroupDTO>(group);
            A.CallTo(() => _groupService.AddItem(group)).Returns(group);
            #endregion
            #region Act
            var result = _groupController.AddGroup(createGroup);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (result as OkObjectResult)!.Value
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void GroupController_DeleteGroup_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>(); ;
            _fieldController.ControllerContext.HttpContext.Items["Player"] = player;
            var group = A.Fake<Group>();
            A.CallTo(() => _groupService.RemoveItem(group)).Returns(true);
            A.CallTo(() => _groupService.IsOwnedBy(group.Id, player.Id)).Returns(true);
            A.CallTo(() => _groupService.DeleteGroup(group.Id)).Returns(true);
            #endregion
            #region Act
            var result = _groupController.DeleteGroup(group.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NoContentResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
        #region MatchController
        [Fact] //*
        public void MatchController_AddMatch_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            _matchController.ControllerContext.HttpContext.Items["Player"] = player;
            var createMatch = A.Fake<CreateMatchDTO>();
            var match = _mapper.Map<Match>(createMatch);
            var matchDTO = _mapper.Map<MatchDTO>(match);
            A.CallTo(() => _matchService.AddItem(match)).Returns(match);
            A.CallTo(() => _matchService.IsValidDate(createMatch.MatchDate)).Returns(true);
            A.CallTo(() => _matchService.AlreadyReserved(match.FieldId, match.MatchDate)).Returns(false);
            A.CallTo(() => _groupService.IsOwnedBy(match.GroupId, player.Id)).Returns(true);
            #endregion
            #region Act
            var result = _matchController.AddMatch(createMatch);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (result as OkObjectResult)!.Value
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void MatchController_DeleteMatch_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            _matchController.ControllerContext.HttpContext.Items["Player"] = player;
            var match = A.Fake<Match>();
            A.CallTo(() => _matchService.GetOwnedBy(match.Id, player.Id)).Returns(match);
            A.CallTo(() => _matchService.RemoveItem(match)).Returns(true);
            #endregion
            #region Act
            var result = _matchController.DeleteMatch(match.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NoContentResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
        #region MessageController
        [Fact] //*
        public void MessageController_AddMessage_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            _messageController.ControllerContext.HttpContext.Items["Player"] = player;
            var createMessage = A.Fake<CreateMessageDTO>();
            var thread = A.Fake<Thread>();
            var message = _mapper.Map<Message>(createMessage);
            var messageDTO = _mapper.Map<MessageDTO>(message);
            A.CallTo(() => _messageService.AddItem(message)).Returns(message);
            A.CallTo(() => _threadService.GetItem(createMessage.ThreadId)).Returns(thread);
            A.CallTo(() => _groupService.IsMember(thread.GroupId, player.Id)).Returns(true);
            #endregion
            #region Act
            var result = _messageController.CreateMessage(createMessage);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (result as OkObjectResult)!.Value
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void MessageController_DeleteMessage_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            _messageController.ControllerContext.HttpContext.Items["Player"] = player;
            var message = A.Fake<Message>();
            message.IsInitial = false;
            A.CallTo(() => _messageService.GetItem(message.Id)).Returns(message);
            A.CallTo(() => _messageService.RemoveItem(message)).Returns(true);
            A.CallTo(() => _messageService.IsOwnedBy(message.Id, player.Id)).Returns(true);
            #endregion
            #region Act
            var result = _messageController.DeleteMessage(message.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NoContentResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
        #region PlayerController
        [Fact]
        public void PlayerController_GetPlayers_ReturnsSuccess()
        {
            #region Arrange
            var players = A.Fake<IEnumerable<Player>>();
            A.CallTo(() => _playerService.GetItems()).Returns(players.ToList());
            #endregion

            #region Act
            List<PlayerDTO>? expectedPlayers = _playerController.GetPlayers().ToList();
            #endregion

            #region Assert
            expectedPlayers.Should().BeOfType<List<PlayerDTO>>()
                .Should().NotBeNull();
            #endregion
        }
        
        [Fact]
        public void PlayerController_GetPlayers_ReturnsEmpty()
        {
            #region Arrange
            A.CallTo(() => _playerService.GetItems()).Returns(new List<Player>());
            #endregion

            #region Act
            var expectedPlayers = _playerController.GetPlayers();
            var finalResult = expectedPlayers.ToList();
            #endregion

            #region Assert
            finalResult.Should().BeEmpty();
            #endregion
        }
        [Fact]
        public void PlayerController_GetPlayer_ReturnsOk()
        {
            #region Arrange
            int id = 1;
            var player = A.Fake<Player>();
            var playerDTO = _mapper.Map<PlayerDTO>(player);
            #endregion
            #region Act
            var result = _playerController.GetPlayer(id);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (result as OkObjectResult)!.Value
                .Should().NotBeNull();
            #endregion
        }

        [Fact]
        public void PlayerController_GetPlayer_ReturnsNotFound()
        {
            #region Arrange
            A.CallTo(() => _playerService.GetItem(-1)).Returns(null);
            #endregion
            #region Act
            var result = _playerController.GetPlayer(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>();
            #endregion
        }
        [Fact] //*
        public void PlayerController_UpdatePlayer_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            var loggedPlayer = new LoggedPlayerDTO(player, "MyToken");
            var updatePlayer = A.Fake<UpdatePlayerDTO>();
            _playerController.ControllerContext.HttpContext.Items["Player"] = player;
            A.CallTo(() => _playerService.UpdateItem(player)).Returns(true);
            A.CallTo(() => _jwtUtils.GenerateJwtToken(player)).Returns("MyToken");
            #endregion
            #region Act
            var result = _playerController.UpdatePlayer(updatePlayer);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (result as OkObjectResult)!.Value
                .Should().BeOfType<LoggedPlayerDTO>()
                .Should().NotBeNull();
            (result as OkObjectResult)!.Value
                .Should().BeEquivalentTo(loggedPlayer);
            #endregion
        }
        [Fact]
        public void PlayerController_UpdatePlayer_ReturnsUnauthorized()
        {
            #region Arrange
            _playerController.ControllerContext.HttpContext.Items["Player"] = null;
            #endregion
            #region Act
            var result = _playerController.UpdatePlayer(new UpdatePlayerDTO());
            #endregion
            #region Assert
            result
                .Should().BeOfType<UnauthorizedObjectResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void PlayerController_RegisterPlayer_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            var registerPlayer = A.Fake<PlayerRegisterDTO>();
            registerPlayer.Password = "523rtfdsd";
            A.CallTo(() => _playerService.AddItem(player)).Returns(player);
            #endregion
            #region Act
            var result = _playerController.RegisterPlayer(registerPlayer);
            #endregion
            #region Assert
            result.Should().BeOfType<CreatedResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void PlayerController_DeletePlayer_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            _playerController.ControllerContext.HttpContext.Items["Player"] = player;
            A.CallTo(() => _playerService.RemoveItem(player)).Returns(true);
            #endregion
            #region Act
            var result = _playerController.DeletePlayer();
            #endregion
            #region Assert
            result.Should().BeOfType<NoContentResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
        #region ThreadController
        [Fact]
        public void ThreadController_GetThread_ReturnsOk()
        {
            #region Arrange
            var thread = A.Fake<Thread>();
            var player = A.Fake<Player>();
            _threadController.ControllerContext.HttpContext.Items["Player"] = player;
            A.CallTo(() => _threadService.GetWithMessages(thread.Id)).Returns(thread);
            A.CallTo(() => _groupService.IsMember(thread.GroupId, player.Id)).Returns(true);
            #endregion
            #region Act
            var result = _threadController.GetThread(thread.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (result as OkObjectResult)!.Value
                .Should().NotBeNull();
            #endregion
        }

        [Fact]
        public void ThreadController_GetThread_ReturnsNotFound()
        {
            #region Arrange
            _threadController.ControllerContext.HttpContext.Items["Player"] = A.Fake<Player>();
            A.CallTo(() => _threadService.GetWithMessages(-1)).Returns(null);
            #endregion
            #region Act
            var result = _threadController.GetThread(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            #endregion
        }
        [Fact] //*
        public void ThreadController_AddThread_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            var createThread = A.Fake<CreateThreadDTO>();
            var thread = _mapper.Map<Thread>(createThread);
            _threadController.ControllerContext.HttpContext.Items["Player"] = player;
            A.CallTo(() => _threadService.AddItem(thread)).Returns(thread);
            A.CallTo(() => _groupService.IsMember(thread.GroupId, player.Id)).Returns(true);
            #endregion
            #region Act
            var result = _threadController.CreateThread(createThread);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            (result as OkObjectResult)!.Value
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void ThreadController_DeleteThread_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            var thread = A.Fake<Thread>();
            _threadController.ControllerContext.HttpContext.Items["Player"] = player;
            A.CallTo(() => _threadService.RemoveItem(thread)).Returns(true);
            A.CallTo(() => _groupService.IsOwnedBy(thread.GroupId, player.Id)).Returns(true);
            #endregion
            #region Act
            var result = _threadController.DeleteThread(thread.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NoContentResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
    }
}