using FitVerse.Core.Interfaces;
using FitVerse.Core.IService;
using FitVerse.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IUnitOfWorkServices
{
    public interface IUnitOFWorkService
    {
        // 🧩 Services
        IClientService ClientService { get; }
        ICoachService CoachService { get; }
        IImageHandleService ImageHandleService { get; }
        IUsersService UsersService { get; }
        IAdminService AdminService { get; }
        IAccountService AccountService { get; }

        IClientOnCoachesService clientOnCoachesService { get; }

        // 🧩 Repositories (اختياري، لو محتاج توصل ليها مباشرة)
        IEquipmentRepository EquipmentRepository { get; }
        IAnatomyRepository AnatomyRepository { get; }
        IMuscleRepository MuscleRepository { get; }
        ICoachRepository CoachRepository { get; }
        IClientRepository ClientRepository { get; }

        ISpecialtiesRepository SpecialtiesRepository { get; }

        ICoachSpecialtiesRepository CoachSpecialtiesRepository { get; }
        
        IExercisePlanDetailRepository ExercisePlanDetailRepository { get; }
        IExercisePlanRepository ExercisePlanRepository { get; }
        
   
        //Identity
        UserManager<ApplicationUser> UserManager { get; }

    }
}
