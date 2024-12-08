using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Passports.Models.Configuration
{
    /// <summary>
    /// Represents UssrPassportHistory table configuration.
    /// </summary>
    public class UssrPassportHistoryConfiguration : IEntityTypeConfiguration<UssrPassportHistory>
    {
        public void Configure(EntityTypeBuilder<UssrPassportHistory> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("ussrpassporthistory");
            entityTypeBuilder.Property(x => x.Id).HasColumnName("id");
            entityTypeBuilder.Property(x => x.PassportSeries).HasColumnName("passp_series");
            entityTypeBuilder.Property(x => x.PassportNumber).HasColumnName("passp_number");
            entityTypeBuilder.Property(x => x.ActiveStart).HasColumnName("active_start");
            entityTypeBuilder.Property(x => x.ActiveEnd).HasColumnName("active_end");
            entityTypeBuilder.HasOne(p => p.UssrPassport).WithMany(s => s.UssrPassportHistories).HasForeignKey(p => new { p.PassportSeries, p.PassportNumber });
        }
    }
}
