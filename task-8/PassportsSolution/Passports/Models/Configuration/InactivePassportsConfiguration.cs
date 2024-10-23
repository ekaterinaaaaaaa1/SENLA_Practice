using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Passports.Models.Configuration
{
    public class InactivePassportsConfiguration : IEntityTypeConfiguration<Passport>
    {
        public void Configure(EntityTypeBuilder<Passport> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Series).HasColumnType("char(4)");
            entityTypeBuilder.Property(x => x.Number).HasColumnType("char(6)");
            entityTypeBuilder.Property(x => x.Series).HasColumnName("passp_series");
            entityTypeBuilder.Property(x => x.Number).HasColumnName("passp_number");
            entityTypeBuilder.Property(x => x.Id).HasColumnName("id");
        }
    }
}
