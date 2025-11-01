using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IMessageService : IService
    {
        Task<Message> CreateAsync(Message message);
        Task<Message> GetByIdAsync(int id);
        Task<Message> UpdateAsync(Message message);
        Task<IEnumerable<Message>> GetChatMessagesAsync(int chatId);
        Task<IEnumerable<Message>> GetUnreadMessagesAsync(string userId);
        Task MarkMessagesAsReadAsync(int chatId, string userId);
        Task<Message> GetLatestMessageInChatAsync(int chatId);
    }
}
