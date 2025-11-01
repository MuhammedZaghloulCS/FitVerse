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
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            _unitOfWork.Notifications.Add(notification);
            await _unitOfWork.SaveAsync();
            return notification;
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _unitOfWork.Notifications.GetQueryable()
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(string userId)
        {
            return await _unitOfWork.Notifications.GetQueryable()
                .Where(n => n.ReciverId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Notification>> GetUnreadNotificationsAsync(string userId)
        {
            return await _unitOfWork.Notifications.GetQueryable()
                .Where(n => n.ReciverId == userId && !n.IsRead)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _unitOfWork.Notifications.GetQueryable()
                .Where(n => n.ReciverId == userId && !n.IsRead)
                .CountAsync();
        }

        public async Task<bool> MarkAsReadAsync(int notificationId, string userId)
        {
            var notification = await _unitOfWork.Notifications.GetQueryable()
                .Where(n => n.Id == notificationId && n.ReciverId == userId)
                .FirstOrDefaultAsync();
            
            if (notification != null)
            {
                notification.IsRead = true;
                _unitOfWork.Notifications.Update(notification);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            var notifications = await _unitOfWork.Notifications.GetQueryable()
                .Where(n => n.ReciverId == userId && !n.IsRead)
                .ToListAsync();
            
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                _unitOfWork.Notifications.Update(notification);
            }
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notification = await _unitOfWork.Notifications.GetQueryable()
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();
            
            if (notification != null)
            {
                _unitOfWork.Notifications.Delete(notification);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Notification>> GetRecentNotificationsAsync(string userId, int count = 10)
        {
            var notifications = await GetUserNotificationsAsync(userId);
            return notifications.Take(count).ToList();
        }
    }
}
