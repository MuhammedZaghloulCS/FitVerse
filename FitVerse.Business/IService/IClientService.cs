using FitVerse.Core.ViewModels.Client;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;

namespace FitVerse.Core.IService
{
    public interface IClientService : IService
    {
        (bool Success, string Message) AddClient(AddClientVM model);
        (bool Success, string Message) UpdateClient(AddClientVM model); // تحديث بيانات العميل
        (bool Success, string Message) DeleteClient(string clientId);    // حذف العميل
        Client? GetById(string clientId);                                // جلب العميل بالـ Id
        List<ClientDashVM> GetAllClients();                              // كل العملاء
        List<ClientDashVM> GetClientsByCoachId(string coachId);          // كل العملاء لكوتش معين
        List<DailyLog> GetClientLogs(string clientId);                   // سجلات العميل اليومية
    }
}
