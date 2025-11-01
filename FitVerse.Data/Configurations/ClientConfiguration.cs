using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FitVerse.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Gender)
                   .HasMaxLength(10);

            builder.Property(c => c.Goal)
                   .HasMaxLength(255);

            builder.Property(c => c.Image)
                   .HasMaxLength(255);

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

            //// Seed sample clients
            //builder.HasData(
            //    new Client
            //    {
            //        Id = "CL1",
            //        Name = "Alice Brown",
            //        Age = 28,
            //        Height = 165.0,
            //        StartWeight = 68.0,
            //        Goal = "Lose weight",
            //        Gender = "Female",
            //        Image = "/images/clients/alice.jpg",
            //        JoinDate = new DateTime(2025, 1, 10),
            //        IsActive = true,
            //        UserId = null
            //    },
            //    new Client
            //    {
            //        Id = "CL2",
            //        Name = "Mohamed Ali",
            //        Age = 34,
            //        Height = 178.0,
            //        StartWeight = 82.5,
            //        Goal = "Build muscle",
            //        Gender = "Male",
            //        Image = "/images/clients/mohamed.jpg",
            //        JoinDate = new DateTime(2025, 3, 1),
            //        IsActive = true,
            //        UserId = null
            //    },
            //    new Client
            //    {
            //        Id = "CL3",
            //        Name = "Sara Lopez",
            //        Age = 22,
            //        Height = 160.0,
            //        StartWeight = 58.0,
            //        Goal = "Tone up",
            //        Gender = "Female",
            //        Image = "/images/clients/sara.jpg",
            //        JoinDate = new DateTime(2024, 10, 15),
            //        IsActive = false,
            //        UserId = null
            //    }
            //);
        }
    }
}