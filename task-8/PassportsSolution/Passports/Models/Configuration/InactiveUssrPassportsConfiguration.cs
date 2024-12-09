using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Passports.Models.Configuration
{
    /// <summary>
    /// Represents InactiveUssrPassports table configuration.
    /// </summary>
    public class InactiveUssrPassportsConfiguration : IEntityTypeConfiguration<UssrPassport>
    {
        public void Configure(EntityTypeBuilder<UssrPassport> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("inactiveussrpassports");
            entityTypeBuilder.Property(x => x.Series).HasColumnType("varchar(9)");
            entityTypeBuilder.Property(x => x.Series).HasColumnName("passp_series");
            entityTypeBuilder.Property(x => x.Number).HasColumnName("passp_number");
            entityTypeBuilder.Property(x => x.IsActive).HasColumnName("active");
            entityTypeBuilder.HasKey(x => new { x.Series, x.Number });
        }
    }
}
