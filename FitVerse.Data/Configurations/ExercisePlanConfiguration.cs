using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Configurations
{
    public class ExercisePlanConfiguration : IEntityTypeConfiguration<ExercisePlan>
    {
        public void Configure(EntityTypeBuilder<ExercisePlan> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

        

            builder.HasMany(e => e.ExercisePlanDetails)
                   .WithOne(d => d.ExercisePlan)
                   .HasForeignKey(d => d.ExercisePlanId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
