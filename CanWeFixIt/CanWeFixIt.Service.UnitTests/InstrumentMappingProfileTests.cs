using AutoFixture;
using AutoMapper;
using CanWeFixIt.Core.Models.Entities;
using CanWeFixIt.Core.Models.Dtos;
using CanWeFixIt.Service.MappingProfiles;
using FluentAssertions;
using Xunit;

namespace CanWeFixIt.Service.Tests
{
    public class InstrumentMappingProfileTests
    {
        private readonly Fixture _autoFixture = new();

        [Fact]
        [Trait("Category", "Unit")]
        public void Map_WhenAnInstrumentIsGiven_ShouldReturnAnInstrumentDto()
        {
            //Arrange
            long arbitraryId = _autoFixture.Create<long>();
            string arbitrarySedol = _autoFixture.Create<string>();
            string arbitraryName = _autoFixture.Create<string>();
            bool arbitraryIsActive = _autoFixture.Create<bool>();

            var instrument = new Instrument { Id = arbitraryId, Sedol = arbitrarySedol, Name = arbitraryName, IsActive = arbitraryIsActive };

            var config = new MapperConfiguration(cfg => cfg.AddProfile<InstrumentMappingProfile>());
            var sut = config.CreateMapper();

            //Act
            var actual = sut.Map<InstrumentDto>(instrument);

            //Assert
            actual.Id.Should().Be(arbitraryId);
            actual.Sedol.Should().Be(arbitrarySedol);
            actual.Name.Should().Be(arbitraryName);
            actual.Active.Should().Be(arbitraryIsActive);
        }

    }
}
