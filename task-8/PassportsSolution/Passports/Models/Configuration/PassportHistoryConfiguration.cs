using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Passports.Models.Configuration
{
    public class PassportHistoryConfiguration : IEntityTypeConfiguration<PassportHistory>
    {
        public void Configure(EntityTypeBuilder<PassportHistory> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("passporthistory");
            //entityTypeBuilder.Property(x => x.Id).HasColumnType("integer");
            entityTypeBuilder.Property(x => x.PassportSeries).HasColumnType("smallint");
            entityTypeBuilder.Property(x => x.PassportNumber).HasColumnType("integer");
            entityTypeBuilder.Property(x => x.ActiveStart).HasColumnType("date");
            entityTypeBuilder.Property(x => x.ActiveEnd).HasColumnType("date");
            entityTypeBuilder.Property(x => x.Id).HasColumnName("id");
            entityTypeBuilder.Property(x => x.PassportSeries).HasColumnName("passp_series");
            entityTypeBuilder.Property(x => x.PassportNumber).HasColumnName("passp_number");
            entityTypeBuilder.Property(x => x.ActiveStart).HasColumnName("active_start");
            entityTypeBuilder.Property(x => x.ActiveEnd).HasColumnName("active_end");
            entityTypeBuilder.HasOne(p => p.Passport).WithMany(s => s.PassportHistories).HasForeignKey(p => new { p.PassportSeries, p.PassportNumber});
        }
    }
}
