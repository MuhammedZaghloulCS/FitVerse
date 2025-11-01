using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Message> CreateAsync(Message message)
        {
            _unitOfWork.Messages.Add(message);
            await _unitOfWork.SaveAsync();
            return message;
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _unitOfWork.Messages.GetQueryable()
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Message> UpdateAsync(Message message)
        {
            _unitOfWork.Messages.Update(message);
            await _unitOfWork.SaveAsync();
            return message;
        }

        public async Task<IEnumerable<Message>> GetChatMessagesAsync(int chatId)
        {
            return await _unitOfWork.Messages.GetQueryable()
                .Where(m => m.ChatId == chatId)
                .Include(m => m.Sender)
                .Include(m => m.Reciver)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetUnreadMessagesAsync(string userId)
        {
            return await _unitOfWork.Messages.GetQueryable()
                .Where(m => m.ReciverId == userId && !m.IsRead)
                .Include(m => m.Sender)
                .Include(m => m.Chat)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();
        }

        public async Task MarkMessagesAsReadAsync(int chatId, string userId)
        {
            var unreadMessages = await _unitOfWork.Messages.GetQueryable()
                .Where(m => m.ChatId == chatId && m.ReciverId == userId && !m.IsRead)
                .ToListAsync();

            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<Message> GetLatestMessageInChatAsync(int chatId)
        {
            return await _unitOfWork.Messages.GetQueryable()
                .Where(m => m.ChatId == chatId)
                .Include(m => m.Sender)
                .OrderByDescending(m => m.SentAt)
                .FirstOrDefaultAsync();
        }
    }
}
