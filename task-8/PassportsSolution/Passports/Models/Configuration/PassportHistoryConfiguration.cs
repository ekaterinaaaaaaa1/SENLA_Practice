using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Passports.Models.Configuration
{
    public class PassportHistoryConfiguration : IEntityTypeConfiguration<PassportHistory>
    {
        public void Configure(EntityTypeBuilder<PassportHistory> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("passporthistory");
            entityTypeBuilder.Property(x => x.Id).HasColumnType("integer");
            entityTypeBuilder.Property(x => x.PassportsSeries).HasColumnType("smallint");
            entityTypeBuilder.Property(x => x.PassportsNumber).HasColumnType("integer");
            entityTypeBuilder.Property(x => x.ActiveStart).HasColumnType("date");
            entityTypeBuilder.Property(x => x.ActiveEnd).HasColumnType("date");
            entityTypeBuilder.Property(x => x.Id).HasColumnName("id");
            entityTypeBuilder.Property(x => x.PassportsSeries).HasColumnName("passp_series");
            entityTypeBuilder.Property(x => x.PassportsNumber).HasColumnName("passp_number");
            entityTypeBuilder.Property(x => x.ActiveStart).HasColumnName("active_start");
            entityTypeBuilder.Property(x => x.ActiveEnd).HasColumnName("active_end");
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.HasOne(p => p.Passport).WithMany(s => s.PassportHistories).HasForeignKey(p => new { p.PassportsSeries, p.PassportsNumber});
        }
    }
}
