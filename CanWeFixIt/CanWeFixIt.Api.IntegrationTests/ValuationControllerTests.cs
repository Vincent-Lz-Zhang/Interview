using CanWeFixIt.Core.Models.Dtos;
using CanWeFixIt.Service.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace CanWeFixIt.Api.IntegrationTests
{
    public class ValuationControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ValuationControllerTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/v1/valuations");
            _factory = factory;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task GetTotalValuation_ShouldReturnsExpectedResponse()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<CanWeFixItContext>));

                    services.Remove(descriptor);

                    string dbConnString = "Data Source=" + Environment.CurrentDirectory + @"\Data\CanWeFixIt.db";
                    services.AddDbContext<CanWeFixItContext>(opt => opt.UseSqlite(
                        dbConnString,
                        sqlOptions => sqlOptions
                            .CommandTimeout(300)), 0);
                    
                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<CanWeFixItContext>();

                    CanWeFixItContextSeeder.SeedForIntegrationTests(dbContext);
                });
            }).CreateClient();

            // Act
            var model = await client.GetFromJsonAsync<IEnumerable<MarketValuationDto>>("");

            // Assert
            model.Should().BeEquivalentTo(
                new List<MarketValuationDto> { 
                    new MarketValuationDto { 
                        Name = "DataValueTotal", 
                        Total = 13332 
                    } 
                }
            );
        }
    }
}
