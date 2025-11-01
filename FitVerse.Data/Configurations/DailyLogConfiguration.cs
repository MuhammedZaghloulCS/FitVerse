using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FitVerse.Data.Models;

public class DailyLogConfiguration : IEntityTypeConfiguration<DailyLog>
{
    public void Configure(EntityTypeBuilder<DailyLog> builder)
    {
        builder.ToTable("DailyLogs");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.ClientId)
            .IsRequired();

        builder.Property(d => d.CoachId)
            .IsRequired();

        builder.Property(d => d.CurrentWeight)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(d => d.ClientNotes)
            .IsRequired()
            .HasMaxLength(1000);

        //builder.Property(d => d.PhotoPath)
        //    .HasMaxLength(255);

        //builder.Property(d => d.CoachFeedback)
        //    .HasMaxLength(1000);


        builder.Property(d => d.IsReviewed)
            .HasDefaultValue(false);

        builder.Property(d => d.LogDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(d => d.Client)
            .WithMany(c => c.DailyLogs)
            .HasForeignKey(d => d.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Coach)
            .WithMany(c => c.DailyLogs)
            .HasForeignKey(d => d.CoachId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
