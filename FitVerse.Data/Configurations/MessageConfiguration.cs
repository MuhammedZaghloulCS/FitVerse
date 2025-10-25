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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Content)
                   .IsRequired()
                   .HasMaxLength(1000);


            builder.Property(m => m.SentAt)
                   .HasDefaultValueSql("GETDATE()");
            builder.HasOne(m => m.Chat)
                   .WithMany(c => c.Messages)
                   .HasForeignKey(m => m.ChatId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Sender)
                   .WithMany() 
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Reciver)
                   .WithMany() 
                   .HasForeignKey(m => m.ReciverId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
