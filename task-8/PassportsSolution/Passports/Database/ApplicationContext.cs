using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Passports.Models;

namespace Passports.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Passport> inactivepassports { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passport>(PassportConfigure);
        }

        public void PassportConfigure(EntityTypeBuilder<Passport> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Series).HasColumnType("char(4)");
            entityTypeBuilder.Property(x => x.Number).HasColumnType("char(6)");
            entityTypeBuilder.Property(x => x.Series).HasColumnName("passp_series");
            entityTypeBuilder.Property(x => x.Number).HasColumnName("passp_number");
           // entityTypeBuilder.Property(x => x.ID).HasColumnType("SERIAL");
            entityTypeBuilder.Property(x => x.Id).HasColumnName("id");
            // entityTypeBuilder.HasKey(x => new { x.Series, x.Number });
        }
    }
}
