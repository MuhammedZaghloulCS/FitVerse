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
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Client)
                   .WithMany(c => c.Chats)
                   .HasForeignKey(c => c.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Coach)
                   .WithMany(c => c.Chats)
                   .HasForeignKey(c => c.CoachId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Messages)
                   .WithOne(m => m.Chat)
                   .HasForeignKey(m => m.ChatId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
