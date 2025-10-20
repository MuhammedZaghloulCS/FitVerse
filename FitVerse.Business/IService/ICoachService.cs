using FitVerse.Core.UnitOfWork;
using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface ICoachService:IService
    {
        (bool Success, string Message) AddCoach(AddCoachVM model); //برتجع تابل tuble    }
        List<AddCoachVM> GetAllCoaches();
        public AddCoachVM GetCoachByIdGuid(Guid id);
        (bool Success, string Message) DeleteCoachById(Guid id);
        (bool Success, string Message) UpdateCoach(AddCoachVM model);
        public (List<AddCoachVM> Data, int TotalItems) GetPagedEquipments(int page, int pageSize, string? search);
        CoachDashboardViewModel GetDashboardData(Guid coachId);
        
       



    }
}
