using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class ClientOnCoachesService : IClientOnCoachesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientOnCoachesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ClientsVM> GetAllClients()
        {

          var clients = _unitOfWork.Clients.GetAll();

          return clients.Select(m => new ClientsVM
        {
        Id = m.Id,
        Name = m.Name,
        Age = m.Age,
        Height = m.Height,
        StartWeight = m.StartWeight,
        Goal = m.Goal,
        Gender = m.Gender,
        Image = m.Image,
        JoinDate = m.JoinDate,
        IsActive = m.IsActive,
        TotalWorkouts = m.ExercisePlans.Count,
        ProgressPercentage = 75,
        //SubscriptionName = m.ClientSubscriptions.FirstOrDefault()?.Subscription?.Name ?? "No Plan"
    }).ToList();
        }
    }
}
