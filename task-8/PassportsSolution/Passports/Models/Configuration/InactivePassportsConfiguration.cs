using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Passports.Models.Configuration
{
    public class InactivePassportsConfiguration : IEntityTypeConfiguration<Passport>
    {
        public void Configure(EntityTypeBuilder<Passport> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("inactivepassports");
            entityTypeBuilder.Property(x => x.Series).HasColumnType("smallint");
            entityTypeBuilder.Property(x => x.Number).HasColumnType("integer");
            entityTypeBuilder.Property(x => x.IsActive).HasColumnType("boolean");
            entityTypeBuilder.Property(x => x.Series).HasColumnName("passp_series");
            entityTypeBuilder.Property(x => x.Number).HasColumnName("passp_number");
            entityTypeBuilder.Property(x => x.IsActive).HasColumnName("active");
            entityTypeBuilder.HasKey(x => new { x.Series, x.Number });
        }
    }
}
