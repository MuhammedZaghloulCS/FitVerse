using FitVerse.Core.Interfaces;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Context;
using System;

namespace FitVerse.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitVerseDbContext _context;

        public UnitOfWork(
            FitVerseDbContext context,
            IMuscleRepository muscles,
            //ICoachRepository coaches,
            //IClientRepository clients,
            IAnatomyRepository anatomies,
            //IMessageRepository messages,
            //IChatRepository chats,
            //IDietPlanRepository dietPlans,
            //IExerciseRepository exercises,
            //IPackageRepository packages,
            //IPaymentRepository payments,
            //INotificationRepository notifications,
            //ICoachSpecialtiesRepository coachSpecialties,
            //ICoachFeedbackRepository coachFeedbacks,
            IEquipmentRepository equipments
            //IExercisePlanRepository exercisePlans,
            //IExercisePlanDetailRepository exercisePlanDetails,
            //ISpecialtiesRepository specialties
            )
        {
            _context = context;

            Muscles = muscles;
            //Coaches = coaches;
            //Clients = clients;
            Anatomies = anatomies;
            //Messages = messages;
            //Chats = chats;
            //DietPlans = dietPlans;
            //Exercises = exercises;
            //Packages = packages;
            //Payments = payments;
            //Notifications = notifications;
            //CoachSpecialties = coachSpecialties;
            //CoachFeedbacks = coachFeedbacks;
            Equipments = equipments;
            //ExercisePlans = exercisePlans;
            //ExercisePlanDetails = exercisePlanDetails;
            //Specialties = specialties;
        }

        public IMuscleRepository Muscles { get; }
        //public ICoachRepository Coaches { get; }
        //public IClientRepository Clients { get; }
        public IAnatomyRepository Anatomies { get; }
        //public IMessageRepository Messages { get; }
        //public IChatRepository Chats { get; }
        //public IDietPlanRepository DietPlans { get; }
        //public IExerciseRepository Exercises { get; }
        //public IPackageRepository Packages { get; }
        //public IPaymentRepository Payments { get; }
        //public INotificationRepository Notifications { get; }
        //public ICoachSpecialtiesRepository CoachSpecialties { get; }
        //public ICoachFeedbackRepository CoachFeedbacks { get; }
        public IEquipmentRepository Equipments { get; }
        //public IExercisePlanRepository ExercisePlans { get; }
        //public IExercisePlanDetailRepository ExercisePlanDetails { get; }
        //public ISpecialtiesRepository Specialties { get; }
        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
