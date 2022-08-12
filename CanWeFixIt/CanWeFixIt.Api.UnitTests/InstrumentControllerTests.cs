using AutoFixture;
using CanWeFixIt.Api.Controllers;
using CanWeFixIt.Core.Models.Dtos;
using CanWeFixIt.Service.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CanWeFixIt.Api.Tests
{
    public class InstrumentControllerTests
    {
        private readonly Mock<IInstrumentService> _instrumentServiceMock;
        private readonly Fixture _autoFixture = new();

        public InstrumentControllerTests()
        {
            _instrumentServiceMock = new Mock<IInstrumentService>();
        }

        private InstrumentController CreateSystemUnderTest() => new(_instrumentServiceMock.Object);

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetAllActiveInstruments_GetAction_ShouldReturnIEnumerableOfInstrumentDtoAsModelView()
        {
            // Arrange
            InstrumentController sut = CreateSystemUnderTest();

            long firstArbitaryId = _autoFixture.Create<long>(), 
                 secondArbitaryId = _autoFixture.Create<long>(), 
                 thirdArbitaryId = _autoFixture.Create<long>();
            string firstArbitarySedol = _autoFixture.Create<string>(), 
                   secondArbitarySedol = _autoFixture.Create<string>(), 
                   thirdArbitarySedol = _autoFixture.Create<string>();
            string firstArbitaryName = _autoFixture.Create<string>(), 
                   secondArbitaryName = _autoFixture.Create<string>(), 
                   thirdArbitaryName = _autoFixture.Create<string>();
            bool firstArbitaryActive = true, 
                 secondArbitaryActive = true, 
                 thirdArbitaryActive = true;

            var instruments = new List<InstrumentDto>
            {
                new InstrumentDto { Id = firstArbitaryId, Sedol = firstArbitarySedol, Name = firstArbitaryName, Active = firstArbitaryActive },
                new InstrumentDto { Id = secondArbitaryId, Sedol = secondArbitarySedol, Name = secondArbitaryName, Active = secondArbitaryActive },
                new InstrumentDto { Id = thirdArbitaryId, Sedol = thirdArbitarySedol, Name = thirdArbitaryName, Active = thirdArbitaryActive }
            };

            _instrumentServiceMock.Setup(s => s.ListActiveInstruments(It.IsAny<CancellationToken>())).ReturnsAsync(instruments);

            // Act 
            var actual = await sut.GetAllActiveInstruments(CancellationToken.None);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InstrumentDto>>>(actual);

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var dtos = Assert.IsAssignableFrom<IEnumerable<InstrumentDto>>(okObjectResult.Value);

            dtos.Should().BeEquivalentTo(
                new List<InstrumentDto>
                {
                    new InstrumentDto { Id = firstArbitaryId, Sedol = firstArbitarySedol, Name = firstArbitaryName, Active = firstArbitaryActive },
                    new InstrumentDto { Id = secondArbitaryId, Sedol = secondArbitarySedol, Name = secondArbitaryName, Active = secondArbitaryActive },
                    new InstrumentDto { Id = thirdArbitaryId, Sedol = thirdArbitarySedol, Name = thirdArbitaryName, Active = thirdArbitaryActive }
                });

            _instrumentServiceMock.Verify(s => s.ListActiveInstruments(It.IsAny<CancellationToken>()), Times.Once);
        }

        // TODO: test other actions
    }
}
