using FakeItEasy;
using FluentAssertions;
using Mops_fullstack.Server.Controllers;
using Mops_fullstack.Server.Core;
using Mops_fullstack.Server.Core.Services;
using Mops_fullstack.Server.Datalayer.DTOs;
using Mops_fullstack.Server.Datalayer.IMapperConverter;
using Mops_fullstack.Server.Datalayer.Models;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

namespace Unit_Tests
{
    public class FieldControllerTests
    {
        private IFieldService _fieldService;
        private FieldController _fieldController;
        public FieldControllerTests()
        { 
            _fieldService = A.Fake<IFieldService>();
            _fieldController = new(_fieldService);
        }

        [Fact]
        public void FieldController_GetFields_ReturnsSuccess()
        {
            #region Arrange
            var fields = A.Fake<IEnumerable<Field>>();
            A.CallTo(() => _fieldService.GetItems()).Returns(fields.ToList());
            #endregion

            #region Act
            var expectedFields= _fieldController.GetFields();
            var finalResult = expectedFields.Select(field => MapperConvert<FieldDTO,Field>.ConvertItem(field)).ToList();
            #endregion

            #region Assert
            finalResult.Should().BeOfType<List<Field>>();
            #endregion
        }
    }
}