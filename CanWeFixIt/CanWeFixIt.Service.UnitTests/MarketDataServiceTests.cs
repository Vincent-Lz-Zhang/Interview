using AutoFixture;
using AutoMapper;
using CanWeFixIt.Core.Interfaces;
using CanWeFixIt.Core.Models.Dtos;
using CanWeFixIt.Service.Data;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CanWeFixIt.Service.Tests
{
    public class MarketDataServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMarketDataRepository> _marketDataRepositoryMock;
        private readonly Fixture _autoFixture = new();

        public MarketDataServiceTests()
        {
            _marketDataRepositoryMock = new Mock<IMarketDataRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        private MarketDataService CreateSystemUnderTest() =>
            new(_mapperMock.Object,
                _marketDataRepositoryMock.Object);

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetMarketValuationAsync_WhenRepoReturnsAValuation_ShouldReturnAMarketValuationDto()
        {
            // Arrange
            MarketDataService sut = CreateSystemUnderTest();
            var arbitraryValuation = _autoFixture.Create<long>();
            _mapperMock.Setup(m => m.Map<MarketValuationDto>(It.Is<long>(input => input == arbitraryValuation))).Returns(new MarketValuationDto { Total = arbitraryValuation });
            _marketDataRepositoryMock.Setup(s => s.GetTotalDataValueAsync(It.IsAny<CancellationToken>())).ReturnsAsync(arbitraryValuation);

            // Act
            var actual = await sut.GetMarketValuationAsync();

            // Assert
            actual.Should().Satisfy(
                mv => mv.Total == arbitraryValuation && mv.Name == "DataValueTotal"
            );
            _marketDataRepositoryMock.Verify(r => r.GetTotalDataValueAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(m => m.Map<MarketValuationDto>(It.Is<long>(input => input == arbitraryValuation)), Times.Once);
        }

        // TODO: test other boundary cases and edgy cases
    }
}
