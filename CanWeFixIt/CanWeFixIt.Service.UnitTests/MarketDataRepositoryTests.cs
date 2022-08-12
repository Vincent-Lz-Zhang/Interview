using AutoFixture;
using CanWeFixIt.Core.Models.Entities;
using CanWeFixIt.Service.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CanWeFixIt.Service.Tests
{
    public class MarketDataRepositoryTests
    {
        private readonly Mock<CanWeFixItContext> _dbContextMock;
        private readonly Fixture _autoFixture = new();

        public MarketDataRepositoryTests()
        {
            _dbContextMock = new Mock<CanWeFixItContext>(
                MockBehavior.Default, 
                new DbContextOptions<CanWeFixItContext>());
        }

        private MarketDataRepository CreateSystemUnderTest() => new(_dbContextMock.Object);


        [Theory]
        [InlineData(0, true, 0, true, 0, true, 0)]
        [InlineData(1, true, 10, true, 100, true, 111)]
        [InlineData(1, false, 10, true, 100, true, 110)]
        [InlineData(1, true, 10, false, 100, true, 101)]
        [InlineData(1, true, 10, true, 100, false, 11)]
        [InlineData(1, true, 10, false, 100, false, 1)]
        [InlineData(1, false, 10, true, 100, false, 10)]
        [InlineData(1, false, 10, false, 100, true, 100)]
        [InlineData(1, false, 10, false, 100, false, 0)]
        [Trait("Category", "Unit")]
        public async Task GetTotalDataValueAsync_WhenThereAreActiveAndInactiveMarketDateRecords_ShouldReturnCorrectMarketValuation(
            long firstArbitraryValue,
            bool firstIsActive,
            long secondArbitraryValue,
            bool secondIsActive,
            long thirdArbitraryValue,
            bool thirdIsActive,
            long expected)
        {
            // Arrange
            MarketDataRepository sut = CreateSystemUnderTest();
            var marketDataList = new List<MarketData>	// TODO: Consider Builder pattern
            {
                new MarketData {Id = _autoFixture.Create<long>(), DataValue = firstArbitraryValue, IsActive = firstIsActive },
                new MarketData {Id = _autoFixture.Create<long>(), DataValue = secondArbitraryValue, IsActive = secondIsActive },
                new MarketData {Id = _autoFixture.Create<long>(), DataValue = thirdArbitraryValue, IsActive = thirdIsActive }
            };

            _dbContextMock.SetupGet(x => x.MarketData).Returns(marketDataList.AsQueryable().BuildMockDbSet().Object);

            // Act
            var actual = await sut.GetTotalDataValueAsync();

            // Assert
            actual.Should().Be(expected);
            _dbContextMock.VerifyGet(s => s.MarketData, Times.Once);
        }

        // TODO: test other boundary cases and edgy cases
    }
}
