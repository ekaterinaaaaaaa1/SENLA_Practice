using Microsoft.EntityFrameworkCore;
using Passports.Models;
using Passports.Models.Configuration;

namespace Passports.Database
{
    /// <summary>
    /// Represents DbContext of the application.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// InactivePassports table.
        /// </summary>
        public DbSet<Passport> InactivePassports { get; set; }

        /// <summary>
        /// InactiveUssrPassports table.
        /// </summary>
        public DbSet<UssrPassport> InactiveUssrPassports { get; set; }

        /// <summary>
        /// PassportHistory table.
        /// </summary>
        public DbSet<PassportHistory> PassportHistory { get; set; }

        /// <summary>
        /// UssrPassportHistory table.
        /// </summary>
        public DbSet<UssrPassportHistory> UssrPassportHistory { get; set; }

        /// <summary>
        /// ApplicationContext constructor.
        /// </summary>
        /// <param name="options">DbContextOptions of the application.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InactivePassportsConfiguration());
            modelBuilder.ApplyConfiguration(new InactiveUssrPassportsConfiguration());
            modelBuilder.ApplyConfiguration(new PassportHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new  UssrPassportHistoryConfiguration());
        }
    }
}
