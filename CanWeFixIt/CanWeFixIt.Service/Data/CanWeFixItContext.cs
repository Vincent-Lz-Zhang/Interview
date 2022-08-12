using CanWeFixIt.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanWeFixIt.Service.Data
{
    public class CanWeFixItContext : DbContext
    {
        public CanWeFixItContext(DbContextOptions<CanWeFixItContext> options) : base(options)
        {
        }

        public virtual DbSet<Instrument> Instruments { get; set; }

        public virtual DbSet<MarketData> MarketData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instrument>().ToTable("Instrument").HasKey(i => i.Id);
            modelBuilder.Entity<Instrument>().Property(i => i.Sedol).HasMaxLength(50);
            modelBuilder.Entity<Instrument>(instrument =>
            {
                instrument.HasIndex(i => i.Sedol).HasFilter("[Sedol] <> ''");
            });
            modelBuilder.Entity<Instrument>().Property(i => i.Name).HasMaxLength(250).IsRequired();

            modelBuilder.Entity<MarketData>().ToTable("MarketData").HasKey(m => m.Id);
            modelBuilder.Entity<MarketData>().Property(m => m.Sedol).HasMaxLength(50);
            modelBuilder.Entity<MarketData>(marketData =>
            {
                marketData.HasIndex(md => md.Sedol);
            });
        }

    }
}
