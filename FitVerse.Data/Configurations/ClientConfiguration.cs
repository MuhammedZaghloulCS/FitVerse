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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

    

            builder.HasMany(c => c.ClientSubscriptions)
                        .WithOne(s => s.Client)
                        .HasForeignKey(s => s.ClientId)
                        .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(c => c.ExercisePlans)
                         .WithOne(e => e.Client)
                         .HasForeignKey(e => e.ClientId)
                         .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.DietPlans)
                        .WithOne(e => e.Client)
                        .HasForeignKey(e => e.ClientId)
                        .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Payments)
                   .WithOne(p => p.Client)
                   .HasForeignKey(p => p.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Chats)
                   .WithOne(ch => ch.Client)
                   .HasForeignKey(ch => ch.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
