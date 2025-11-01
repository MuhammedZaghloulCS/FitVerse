using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.Interfaces
{
    public interface ICoachRepository : IGenericRepository<Data.Models.Coach>
    {
        Data.Models.Coach GetCoachByIdGuid(string id);
        (bool Success, string Message) DeleteCoachById(string id);
        public int GetActiveClientsCount();

        public int GetTotalPlans(string coachId);
        public int GetTotalExercises();
        public double GetAverageRating(string coachId);

        public List<ClientDashVM> GetRecentClients(string coachId);
        public List<ClientDashVM> GetAllClientsByCoach(string coachId);
        public void DeleteByUserId(string UserId);
        
        IQueryable<Coach> GetAllWithPackages();
        public List<Specialty> GetCoachspecialtiesByCoachId(string CoachId);



    }
}

    
