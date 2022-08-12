using CanWeFixIt.Core.Models.Entities;
using System.Linq;

namespace CanWeFixIt.Service.Data
{
    public class CanWeFixItContextSeeder
    {
        public static void Seed(CanWeFixItContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (!dbContext.Instruments.Any())
            {
                dbContext.Instruments.AddRange(
                new Instrument { Id = 1, Sedol = "Sedol1", Name = "Name1", IsActive = false },
                new Instrument { Id = 2, Sedol = "Sedol2", Name = "Name2", IsActive = true },
                new Instrument { Id = 3, Sedol = "Sedol3", Name = "Name3", IsActive = false },
                new Instrument { Id = 4, Sedol = "Sedol4", Name = "Name4", IsActive = true },
                new Instrument { Id = 5, Sedol = "Sedol5", Name = "Name5", IsActive = false },
                new Instrument { Id = 6, Sedol = "",       Name = "Name6", IsActive = true },
                new Instrument { Id = 7, Sedol = "Sedol7", Name = "Name7", IsActive = false },
                new Instrument { Id = 8, Sedol = "Sedol8", Name = "Name8", IsActive = true },
                new Instrument { Id = 9, Sedol = "Sedol9", Name = "Name9", IsActive = false });
            }

            if (!dbContext.MarketData.Any())
            {
                dbContext.MarketData.AddRange(
                new MarketData { Id = 1, DataValue = 1111, Sedol = "Sedol1", IsActive = false },
                new MarketData { Id = 2, DataValue = 2222, Sedol = "Sedol2", IsActive = true },
                new MarketData { Id = 3, DataValue = 3333, Sedol = "Sedol3", IsActive = false },
                new MarketData { Id = 4, DataValue = 4444, Sedol = "Sedol4", IsActive = true },
                new MarketData { Id = 5, DataValue = 5555, Sedol = "Sedol5", IsActive = false },
                new MarketData { Id = 6, DataValue = 6666, Sedol = "Sedol6", IsActive = true });
            }

            dbContext.SaveChanges();
        }

        public static void SeedForIntegrationTests(CanWeFixItContext dbContext)
        {
            Seed(dbContext);
        }
    }
}
