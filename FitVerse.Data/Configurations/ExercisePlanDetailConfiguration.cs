using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Configurations
{
    public class ExercisePlanDetailConfiguration : IEntityTypeConfiguration<ExercisePlanDetail>
    {
        public void Configure(EntityTypeBuilder<ExercisePlanDetail> builder)
        {
            builder.HasKey(d => new {d.ExerciseId,d.ExercisePlanId});

            builder.Property(d => d.NumOfSets).IsRequired();
            builder.Property(d => d.NumOfRepeats).IsRequired();

            builder.HasOne(d => d.ExercisePlan)
                   .WithMany(p => p.ExercisePlanDetails)
                   .HasForeignKey(d => d.ExercisePlanId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Exercise)
                   .WithMany(d=>d.ExercisePlanDetails)
                   .HasForeignKey(d => d.ExerciseId)
                   .OnDelete(DeleteBehavior.Restrict);

           

        }
    }
}
