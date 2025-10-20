using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.Interfaces
{
    public interface ICoachRepository : IGenericRepository<Data.Models.Coach>
    {
        Data.Models.Coach GetCoachByIdGuid(Guid id);
        (bool Success, string Message) DeleteCoachById(Guid id);
        public int GetActiveClientsCount();

        public int GetTotalPlans(Guid coachId);
        public int GetTotalExercises();
        public double GetAverageRating(Guid coachId);

        public List<ClientDashVM> GetRecentClients(Guid coachId);
        public List<ClientDashVM> GetAllClientsByCoach(Guid coachId);


    }
}

    
