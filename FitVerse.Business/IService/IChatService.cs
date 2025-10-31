using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IChatService : IService
    {
        Task<Chat> GetChatBetweenUsersAsync(string clientId, string coachId);
        Task<IEnumerable<Chat>> GetUserChatsAsync(string userId);
        Task<Chat> CreateChatAsync(string clientId, string coachId);
    }
}
