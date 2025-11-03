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
        IAnatomyService AnatomyService { get; }
        //IUsers UsersService { get; }
        IEquipmentService EquipmentService { get; }
        IDietPlan DietPlanService { get; }
        IAdminService AdminService { get; }
        IUsersService UsersService { get; }
        IAccountService AccountService { get; }
        IPackageAppService PackageAppService { get; }
        IDailyLogService DailyLogService { get; }
        IDietPlanRepository DietPlanRepository { get; }
        IClientDashboardService ClientDashboardService { get; }

        IClientOnCoachesService clientOnCoachesService { get; }

        // 🧩 Repositories (اختياري، لو محتاج توصل ليها مباشرة)
        IEquipmentRepository EquipmentRepository { get; }
        IAnatomyRepository AnatomyRepository { get; }
        IMuscleRepository MuscleRepository { get; }
        ICoachRepository CoachRepository { get; }
        IClientRepository ClientRepository { get; }
        IPackageRepository PackageRepository { get; }
        ICoachPackageRepository CoachPackageRepository { get; }

        ISpecialtiesRepository SpecialtiesRepository { get; }
        IDailyLogRepository DailyLogRepository {get; }

        ICoachSpecialtiesRepository CoachSpecialtiesRepository { get; }
        
        IExercisePlanDetailRepository ExercisePlanDetailRepository { get; }
        IExercisePlanRepository ExercisePlanRepository { get; }
        IExerciseRepository ExerciseRepository { get; }
        IPaymentRepository PaymentRepository { get; }

        // Generic repositories for direct access
        IGenericRepository<FitVerse.Data.Models.Coach> Coaches { get; }
        IGenericRepository<FitVerse.Data.Models.Client> Clients { get; }

        //Identity
        UserManager<ApplicationUser> UserManager { get; }


    }
}
