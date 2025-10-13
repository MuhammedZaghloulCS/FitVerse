using FitVerse.Core.Interfaces;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {

        IMuscleRepository Muscles { get; }
        ICoachRepository Coaches { get; }
        IClientRepository Clients { get; }
        IAnatomyRepository Anatomies { get; }
        ICoachSpecialtiesRepository CoachSpecialties { get; }
        ICoachFeedbackRepository CoachFeedbacks { get; }
        IDietPlanRepository DietPlans { get; }
        IEquipmentRepository Equipments { get; }
        IExerciseRepository Exercises { get; }
        IMessageRepository Messages { get; }
        IChatRepository Chats { get; }
        IExercisePlanRepository ExercisePlans { get; }
        IExercisePlanDetailRepository ExercisePlanDetails { get; }
        INotificationRepository Notifications { get; }
        IPackageRepository Packages { get; }
        IPaymentRepository Payments { get; }
        ISpecialtiesRepository Specialties { get; }
        int Complete();
    }
}
