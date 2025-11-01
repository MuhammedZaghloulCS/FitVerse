using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface INotificationService
    {
        Task<Notification> CreateAsync(Notification notification);
        Task<Notification> GetByIdAsync(int id);
        Task<List<Notification>> GetUserNotificationsAsync(string userId);
        Task<List<Notification>> GetUnreadNotificationsAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task<bool> MarkAsReadAsync(int notificationId, string userId);
        Task<bool> MarkAllAsReadAsync(string userId);
        Task<bool> DeleteAsync(int id);
        Task<List<Notification>> GetRecentNotificationsAsync(string userId, int count = 10);
    }
}
