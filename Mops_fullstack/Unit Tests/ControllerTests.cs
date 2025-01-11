using System.Drawing;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Mops_fullstack.Server.Controllers;
using Mops_fullstack.Server.Core;
using Mops_fullstack.Server.Core.Services;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;
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
        private FieldController _fieldController;
        private GroupController _groupController;
        private MatchController _matchController;
        private MessageController _messageController;
        private OwnerController _ownerController;
        private PlayerController _playerController;
        private ThreadController _threadController;
        #endregion
        public ControllerTests()
        {
            _fieldService = A.Fake<IFieldService>();
            _groupService = A.Fake<IGroupService>();
            _matchService = A.Fake<IMatchService>();
            _messageService = A.Fake<IMessageService>();
            _ownerService= A.Fake<IOwnerService>();
            _playerService= A.Fake<IPlayerService>();
            _threadService= A.Fake<IThreadService>();
            _fieldController = new(_fieldService);
            _groupController = new(_groupService);
            _matchController = new(_matchService);
            _messageController = new(_messageService);
            _ownerController = new(_ownerService);
            _playerController = new(_playerService);
            _threadController = new(_threadService);
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
            var expectedFields = _fieldController.GetFields();
            var finalResult = expectedFields.Select(field => MapperConvert<FieldDTO, Field>.ConvertItem(field)).ToList();
            #endregion

            #region Assert
            finalResult.Should().BeOfType<List<Field>>()
                .Should().NotBeNull();
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
            var expectedFields = _fieldController.GetFields();
            var finalResult = expectedFields.ToList();
            #endregion

            #region Assert
            finalResult.Should().NotBeOfType<List<Field>>();
            #endregion
        }
        [Fact]
        public void FieldController_GetFields_ReturnsEmpty()
        {
            #region Arrange
            A.CallTo(() => _fieldService.GetItems()).Returns(new List<Field>());
            #endregion

            #region Act
            var expectedFields = _fieldController.GetFields();
            var finalResult = expectedFields.ToList();
            #endregion

            #region Assert
            finalResult.Should().BeEmpty();
            #endregion
        }
        [Fact]
        public void FieldController_GetField_ReturnsOk()
        {
            #region Arrange
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
            Field field = null;
            A.CallTo(() => _fieldService.GetItem(-1)).Returns(field);
            #endregion
            #region Act
            var result = _fieldController.GetField(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>();
            #endregion
        }
        [Fact] //*
        public void FieldController_UpdateField_ReturnsOk()
        {
            #region Arrange
            var field = A.Fake<Field>();
            A.CallTo(() => _fieldService.UpdateItem(field)).Returns(true);
            #endregion
            #region Act
            var result = _fieldController.UpdateField(MapperConvert<Field, FieldDTO>.ConvertItem(field));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void FieldController_UpdateField_ReturnsNotFound()
        {
            #region Arrange
            Field field = null;
            A.CallTo(() => _fieldService.UpdateItem(field)).Returns(false);
            #endregion
            #region Act
            var result = _fieldController.UpdateField(MapperConvert<Field, FieldDTO>.ConvertItem(field));
            #endregion
            #region Assert
            result.Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void FieldController_AddField_ReturnsOk()
        {
            #region Arrange
            var field = A.Fake<Field>();
            A.CallTo(() => _fieldService.AddItem(field)).Returns(true);
            #endregion
            #region Act
            var result = _fieldController.AddField(MapperConvert<Field, FieldDTO>.ConvertItem(field));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void FieldController_DeleteField_ReturnsOk()
        {
            #region Arrange
            var field = A.Fake<Field>();
            A.CallTo(() => _fieldService.RemoveItem(field)).Returns(true);
            #endregion
            #region Act
            var result = _fieldController.DeleteField(field.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>().
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
            var expectedGroups = _groupController.GetGroups();
            var finalResult = expectedGroups.Select(group => MapperConvert<GroupDTO, Group>.ConvertItem(group)).ToList();
            #endregion

            #region Assert
            finalResult.Should().BeOfType<List<Group>>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void GroupController_GetGroups_ReturnsFailure()
        {
            #region Arrange
            var groups = A.Fake<IEnumerable<Group>>();
            A.CallTo(() => _groupService.GetItems()).Returns(groups.ToList());
            #endregion

            #region Act
            var expectedGroups = _groupController.GetGroups();
            var finalResult = expectedGroups.ToList();
            #endregion

            #region Assert
            finalResult.Should().NotBeOfType<List<Group>>();
            #endregion
        }
        [Fact]
        public void GroupController_GetGroups_ReturnsEmpty()
        {
            #region Arrange
            A.CallTo(() => _groupService.GetItems()).Returns(new List<Group>());
            #endregion

            #region Act
            var expectedGroups = _groupController.GetGroups();
            var finalResult = expectedGroups.ToList();
            #endregion

            #region Assert
            finalResult.Should().BeEmpty();
            #endregion
        }
        [Fact]
        public void GroupController_GetGroup_ReturnsOk()
        {
            #region Arrange
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
            Group group = null;
            A.CallTo(() => _groupService.GetItem(-1)).Returns(group);
            #endregion
            #region Act
            var result = _groupController.GetGroup(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>();
            #endregion
        }
        [Fact] //*
        public void GroupController_UpdateGroup_ReturnsOk()
        {
            #region Arrange
            var group = A.Fake<Group>();
            A.CallTo(() => _groupService.UpdateItem(group)).Returns(true);
            #endregion
            #region Act
            var result = _groupController.UpdateGroup(MapperConvert<Group, GroupDTO>.ConvertItem(group));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void GroupController_UpdateGroup_ReturnsNotFound()
        {
            #region Arrange
            Group group = null;
            A.CallTo(() => _groupService.UpdateItem(group)).Returns(false);
            #endregion
            #region Act
            var result = _groupController.UpdateGroup(MapperConvert<Group, GroupDTO>.ConvertItem(group));
            #endregion
            #region Assert
            result.Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void GroupController_AddGroup_ReturnsOk()
        {
            #region Arrange
            var group = A.Fake<Group>();
            A.CallTo(() => _groupService.AddItem(group)).Returns(true);
            #endregion
            #region Act
            var result = _groupController.UpdateGroup(MapperConvert<Group, GroupDTO>.ConvertItem(group));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void GroupController_DeleteGroup_ReturnsOk()
        {
            #region Arrange
            var group = A.Fake<Group>();
            A.CallTo(() => _groupService.RemoveItem(group)).Returns(true);
            #endregion
            #region Act
            var result = _groupController.DeleteGroup(group.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
        #region MatchController
        [Fact]
        public void MatchController_GetMatches_ReturnsSuccess()
        {
            #region Arrange
            var matches = A.Fake<IEnumerable<Match>>();
            A.CallTo(() => _matchService.GetItems()).Returns(matches.ToList());
            #endregion

            #region Act
            var expectedMatches = _matchController.GetMatches();
            var finalResult = expectedMatches.Select(match => MapperConvert<MatchDTO, Match>.ConvertItem(match)).ToList();
            #endregion

            #region Assert
            finalResult.Should().BeOfType<List<Match>>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void MatchController_GetMatches_ReturnsFailure()
        {
            #region Arrange
            var matches = A.Fake<IEnumerable<Match>>();
            A.CallTo(() => _matchService.GetItems()).Returns(matches.ToList());
            #endregion

            #region Act
            var expectedMatches = _matchController.GetMatches();
            var finalResult = expectedMatches.ToList();
            #endregion

            #region Assert
            finalResult.Should().NotBeOfType<List<Match>>();
            #endregion
        }
        [Fact]
        public void MatchController_GetMatches_ReturnsEmpty()
        {
            #region Arrange
            A.CallTo(() => _fieldService.GetItems()).Returns(new List<Field>());
            #endregion

            #region Act
            var expectedFields = _fieldController.GetFields();
            var finalResult = expectedFields.ToList();
            #endregion

            #region Assert
            finalResult.Should().BeEmpty();
            #endregion
        }
        [Fact]
        public void MatchController_GetMatch_ReturnsOk()
        {
            #region Arrange
            int id = 1;
            var match = A.Fake<Match>();
            A.CallTo(() => _matchService.GetItem(id)).Returns(match);
            #endregion
            #region Act
            var result = _matchController.GetMatch(id);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            #endregion
        }

        [Fact]
        public void MatchController_GetMatch_ReturnsNotFound()
        {
            #region Arrange
            Match match = null;
            A.CallTo(() => _matchService.GetItem(-1)).Returns(match);
            #endregion
            #region Act
            var result = _matchController.GetMatch(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>();
            #endregion
        }
        [Fact] //*
        public void MatchController_UpdateMatch_ReturnsOk()
        {
            #region Arrange
            var match = A.Fake<Match>();
            A.CallTo(() => _matchService.UpdateItem(match)).Returns(true);
            #endregion
            #region Act
            var result = _matchController.UpdateMatch(MapperConvert<Match, MatchDTO>.ConvertItem(match));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void MatchController_UpdateMatch_ReturnsNotFound()
        {
            #region Arrange
            Match match = null;
            A.CallTo(() => _matchService.UpdateItem(match)).Returns(false);
            #endregion
            #region Act
            var result = _matchController.UpdateMatch(MapperConvert<Match,MatchDTO>.ConvertItem(match));
            #endregion
            #region Assert
            result.Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void MatchController_AddMatch_ReturnsOk()
        {
            #region Arrange
            var match = A.Fake<Match>();
            A.CallTo(() => _matchService.AddItem(match)).Returns(true);
            #endregion
            #region Act
            var result = _matchController.AddMatch(MapperConvert<Match, MatchDTO>.ConvertItem(match));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void MatchController_DeleteMatch_ReturnsOk()
        {
            #region Arrange
            var match = A.Fake<Match>();
            A.CallTo(() => _matchService.RemoveItem(match)).Returns(true);
            #endregion
            #region Act
            var result = _matchController.DeleteMatch(match.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
        #region MessageController
        [Fact]
        public void MessageController_GetMessages_ReturnsSuccess()
        {
            #region Arrange
            var messages = A.Fake<IEnumerable<Message>>();
            A.CallTo(() => _messageService.GetItems()).Returns(messages.ToList());
            #endregion

            #region Act
            var expectedMessages = _messageController.GetMessages();
            var finalResult = expectedMessages.Select(message => MapperConvert<MessageDTO, Message>.ConvertItem(message)).ToList();
            #endregion

            #region Assert
            finalResult.Should().BeOfType<List<Message>>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void MessageController_GetMessages_ReturnsFailure()
        {
            #region Arrange
            var messages = A.Fake<IEnumerable<Message>>();
            A.CallTo(() => _messageService.GetItems()).Returns(messages.ToList());
            #endregion

            #region Act
            var expectedMessages = _messageController.GetMessages();
            var finalResult = expectedMessages.ToList();
            #endregion

            #region Assert
            finalResult.Should().NotBeOfType<List<Message>>();
            #endregion
        }
        [Fact]
        public void MessageController_GetMessages_ReturnsEmpty()
        {
            #region Arrange
            A.CallTo(() => _messageService.GetItems()).Returns(new List<Message>());
            #endregion

            #region Act
            var expectedMessage = _messageController.GetMessages();
            var finalResult = expectedMessage.ToList();
            #endregion

            #region Assert
            finalResult.Should().BeEmpty();
            #endregion
        }
        [Fact]
        public void MessageController_GetMessage_ReturnsOk()
        {
            #region Arrange
            int id = 1;
            var message = A.Fake<Message>();
            A.CallTo(() => _messageService.GetItem(id)).Returns(message);
            #endregion
            #region Act
            var result = _messageController.GetMessages(id);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            #endregion
        }

        [Fact]
        public void MessageController_GetMessage_ReturnsNotFound()
        {
            #region Arrange
            Message message = null;
            A.CallTo(() => _messageService.GetItem(-1)).Returns(message);
            #endregion
            #region Act
            var result = _messageController.GetMessages(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>();
            #endregion
        }
        [Fact] //*
        public void MessageController_UpdateMessage_ReturnsOk()
        {
            #region Arrange
            var message = A.Fake<Message>();
            A.CallTo(() => _messageService.UpdateItem(message)).Returns(true);
            #endregion
            #region Act
            var result = _messageController.UpdateMessage(MapperConvert<Message, MessageDTO>.ConvertItem(message));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void MessageController_UpdateMessage_ReturnsNotFound()
        {
            #region Arrange
            Message message = null;
            A.CallTo(() => _messageService.UpdateItem(message)).Returns(false);
            #endregion
            #region Act
            var result = _messageController.UpdateMessage(MapperConvert<Message, MessageDTO>.ConvertItem(message));
            #endregion
            #region Assert
            result.Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void MessageController_AddMessage_ReturnsOk()
        {
            #region Arrange
            var message = A.Fake<Message>();
            A.CallTo(() => _messageService.AddItem(message)).Returns(true);
            #endregion
            #region Act
            var result = _messageController.AddMessage(MapperConvert<Message, MessageDTO>.ConvertItem(message));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void MessageController_DeleteMessage_ReturnsOk()
        {
            #region Arrange
            var message = A.Fake<Message>();
            A.CallTo(() => _messageService.RemoveItem(message)).Returns(true);
            #endregion
            #region Act
            var result = _messageController.DeleteMessage(message.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
        #region OwnerController
        [Fact]
        public void OwnerController_GetOwners_ReturnsSuccess()
        {
            #region Arrange
            var owners = A.Fake<IEnumerable<Owner>>();
            A.CallTo(() => _ownerService.GetItems()).Returns(owners.ToList());
            #endregion

            #region Act
            var expectedOwners = _ownerController.GetOwners();
            var finalResult = expectedOwners.Select(owner => MapperConvert<OwnerDTO, Owner>.ConvertItem(owner)).ToList();
            #endregion

            #region Assert
            finalResult.Should().BeOfType<List<Owner>>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void OwnerController_GetOwners_ReturnsFailure()
        {
            #region Arrange
            var owners = A.Fake<IEnumerable<Owner>>();
            A.CallTo(() => _ownerService.GetItems()).Returns(owners.ToList());
            #endregion

            #region Act
            var expectedOwners = _ownerController.GetOwners();
            var finalResult = expectedOwners.ToList();
            #endregion

            #region Assert
            finalResult.Should().NotBeOfType<List<Owner>>();
            #endregion
        }
        [Fact]
        public void OwnerController_GetOwners_ReturnsEmpty()
        {
            #region Arrange
            A.CallTo(() => _ownerService.GetItems()).Returns(new List<Owner>());
            #endregion

            #region Act
            var expectedOwners = _ownerController.GetOwners();
            var finalResult = expectedOwners.ToList();
            #endregion

            #region Assert
            finalResult.Should().BeEmpty();
            #endregion
        }
        [Fact]
        public void OwnersController_GetOwner_ReturnsOk()
        {
            #region Arrange
            int id = 1;
            var owner = A.Fake<Owner>();
            A.CallTo(() => _ownerService.GetItem(id)).Returns(owner);
            #endregion
            #region Act
            var result = _ownerController.GetOwner(id);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            #endregion
        }

        [Fact]
        public void OwnerController_GetOwner_ReturnsNotFound()
        {
            #region Arrange
            Owner owner = null;
            A.CallTo(() => _ownerService.GetItem(-1)).Returns(owner);
            #endregion
            #region Act
            var result = _ownerController.GetOwner(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>();
            #endregion
        }
        [Fact] //*
        public void OwnerController_UpdateOwner_ReturnsOk()
        {
            #region Arrange
            var owner = A.Fake<Owner>();
            A.CallTo(() => _ownerService.UpdateItem(owner)).Returns(true);
            #endregion
            #region Act
            var result = _ownerController.UpdateOwner(MapperConvert<Owner, OwnerDTO>.ConvertItem(owner));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void OwnerController_UpdateOwner_ReturnsNotFound()
        {
            #region Arrange
            Owner owner = null;
            A.CallTo(() => _ownerService.UpdateItem(owner)).Returns(false);
            #endregion
            #region Act
            var result = _ownerController.UpdateOwner(MapperConvert<Owner, OwnerDTO>.ConvertItem(owner));
            #endregion
            #region Assert
            result.Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void OwnerController_AddOwner_ReturnsOk()
        {
            #region Arrange
            var owner = A.Fake<Owner>();
            A.CallTo(() => _ownerService.AddItem(owner)).Returns(true);
            #endregion
            #region Act
            var result = _ownerController.AddOwner(MapperConvert<Owner, OwnerDTO>.ConvertItem(owner));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void OwnerController_DeleteOwner_ReturnsOk()
        {
            #region Arrange
            var owner = A.Fake<Owner>();
            A.CallTo(() => _ownerService.RemoveItem(owner)).Returns(true);
            #endregion
            #region Act
            var result = _ownerController.DeleteOwner(owner.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>().
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
            var expectedPlayers = _playerController.GetPlayers();
            var finalResult = expectedPlayers.Select(player => MapperConvert<PlayerDTO, Player>.ConvertItem(player)).ToList();
            #endregion

            #region Assert
            finalResult.Should().BeOfType<List<Player>>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void PlayerController_GetPlayers_ReturnsFailure()
        {
            #region Arrange
            var players = A.Fake<IEnumerable<Player>>();
            A.CallTo(() => _playerService.GetItems()).Returns(players.ToList());
            #endregion

            #region Act
            var expectedPlayers = _playerController.GetPlayers();
            var finalResult = expectedPlayers.ToList();
            #endregion

            #region Assert
            finalResult.Should().NotBeOfType<List<Player>>();
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
            A.CallTo(() => _playerService.GetItem(id)).Returns(player);
            #endregion
            #region Act
            var result = _playerController.GetPlayer(id);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            #endregion
        }

        [Fact]
        public void PlayerController_GetPlayer_ReturnsNotFound()
        {
            #region Arrange
            Player player = null;
            A.CallTo(() => _playerService.GetItem(-1)).Returns(player);
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
            A.CallTo(() => _playerService.UpdateItem(player)).Returns(true);
            #endregion
            #region Act
            var result = _playerController.UpdatePlayer(MapperConvert<Player, PlayerDTO>.ConvertItem(player));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void PlayerController_UpdatePlayer_ReturnsNotFound()
        {
            #region Arrange
            Player player = null;
            A.CallTo(() => _playerService.UpdateItem(player)).Returns(false);
            #endregion
            #region Act
            var result = _playerController.UpdatePlayer(MapperConvert<Player, PlayerDTO>.ConvertItem(player));
            #endregion
            #region Assert
            result.Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void PlayerController_AddPlayer_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            A.CallTo(() => _playerService.AddItem(player)).Returns(true);
            #endregion
            #region Act
            var result = _playerController.AddPlayer(MapperConvert<Player, PlayerDTO>.ConvertItem(player));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void PlayerController_DeletePlayer_ReturnsOk()
        {
            #region Arrange
            var player = A.Fake<Player>();
            A.CallTo(() => _playerService.RemoveItem(player)).Returns(true);
            #endregion
            #region Act
            var result = _playerController.DeletePlayer(player.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
        #region ThreadController
        [Fact]
        public void ThreadController_GetThreads_ReturnsSuccess()
        {
            #region Arrange
            var threads = A.Fake<IEnumerable<Thread>>();
            A.CallTo(() => _threadService.GetItems()).Returns(threads.ToList());
            #endregion

            #region Act
            var expectedThreads = _threadController.GetThreads();
            var finalResult = expectedThreads.Select(thread => MapperConvert<ThreadDTO, Thread>.ConvertItem(thread)).ToList();
            #endregion

            #region Assert
            finalResult.Should().BeOfType<List<Thread>>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void ThreadController_GetThreads_ReturnsFailure()
        {
            #region Arrange
            var threads = A.Fake<IEnumerable<Thread>>();
            A.CallTo(() => _threadService.GetItems()).Returns(threads.ToList());
            #endregion

            #region Act
            var expectedThread = _threadController.GetThreads();
            var finalResult = expectedThread.ToList();
            #endregion

            #region Assert
            finalResult.Should().NotBeOfType<List<Thread>>();
            #endregion
        }
        [Fact]
        public void ThreadController_GetThreads_ReturnsEmpty()
        {
            #region Arrange
            A.CallTo(() => _threadService.GetItems()).Returns(new List<Thread>());
            #endregion

            #region Act
            var expectedThreads = _threadController.GetThreads();
            var finalResult = expectedThreads.ToList();
            #endregion

            #region Assert
            finalResult.Should().BeEmpty();
            #endregion
        }
        [Fact]
        public void ThreadController_GetThread_ReturnsOk()
        {
            #region Arrange
            int id = 1;
            var thread = A.Fake<Thread>();
            A.CallTo(() => _threadService.GetItem(id)).Returns(thread);
            #endregion
            #region Act
            var result = _threadController.GetThread(id);
            #endregion
            #region Assert
            result.Should().BeOfType<OkObjectResult>()
                .Should().NotBeNull();
            #endregion
        }

        [Fact]
        public void ThreadController_GetThread_ReturnsNotFound()
        {
            #region Arrange
            Thread thread = null;
            A.CallTo(() => _threadService.GetItem(-1)).Returns(thread);
            #endregion
            #region Act
            var result = _threadController.GetThread(-1);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>();
            #endregion
        }
        [Fact] //*
        public void ThreadController_UpdateThread_ReturnsOk()
        {
            #region Arrange
            var thread = A.Fake<Thread>();
            A.CallTo(() => _threadService.UpdateItem(thread)).Returns(true);
            #endregion
            #region Act
            var result = _threadController.UpdateThread(MapperConvert<Thread, ThreadDTO>.ConvertItem(thread));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact]
        public void ThreadController_UpdateThread_ReturnsNotFound()
        {
            #region Arrange
            Thread thread = null;
            A.CallTo(() => _threadService.UpdateItem(thread)).Returns(false);
            #endregion
            #region Act
            var result = _threadController.UpdateThread(MapperConvert<Thread, ThreadDTO>.ConvertItem(thread));
            #endregion
            #region Assert
            result.Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void ThreadController_AddThread_ReturnsOk()
        {
            #region Arrange
            var thread = A.Fake<Thread>();
            A.CallTo(() => _threadService.AddItem(thread)).Returns(true);
            #endregion
            #region Act
            var result = _threadController.AddThread(MapperConvert<Thread, ThreadDTO>.ConvertItem(thread));
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>()
                .Should().NotBeNull();
            #endregion
        }
        [Fact] //*
        public void ThreadController_DeleteThread_ReturnsOk()
        {
            #region Arrange
            var thread = A.Fake<Thread>();
            A.CallTo(() => _threadService.RemoveItem(thread)).Returns(true);
            #endregion
            #region Act
            var result = _threadController.DeleteThread(thread.Id);
            #endregion
            #region Assert
            result.Should().BeOfType<NotFoundResult>().
                Should().NotBeNull();
            #endregion
        }
        #endregion
    }
}