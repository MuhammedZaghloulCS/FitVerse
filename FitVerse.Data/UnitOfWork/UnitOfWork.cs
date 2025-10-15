using FitVerse.Core.Interfaces;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Context;
using FitVerse.Data.Repositories;

namespace FitVerse.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitVerseDbContext _context;

        public UnitOfWork(
            FitVerseDbContext context
            //IMuscleRepository muscles,
            //ICoachRepository coaches,
            //IClientRepository clients,
            
            //IMessageRepository messages,
            //IChatRepository chats,
            //IDietPlanRepository dietPlans,
            //IExerciseRepository exercises,
            //IPackageRepository packages,
            //IPaymentRepository payments,
            //INotificationRepository notifications,
            //ICoachSpecialtiesRepository coachSpecialties,
            //ICoachFeedbackRepository coachFeedbacks,
            
            //IExercisePlanRepository exercisePlans,
            //IExercisePlanDetailRepository exercisePlanDetails,
            //ISpecialtiesRepository specialties
            )
        {
            _context = context;

            Muscles = muscles;
            //Coaches = coaches;
            //Clients = clients;
            AnatomyRepositroy anatomies = new AnatomyRepositroy(_context);
            //Messages = messages;
            //Chats = chats;
            //DietPlans = dietPlans;
            //Exercises = exercises; 
            //Packages = packages;
            //Payments = payments;
            //Notifications = notifications;
            //CoachSpecialties = coachSpecialties;
            //CoachFeedbacks = coachFeedbacks;
            EquipmentRepository equipments=new EquipmentRepository(_context);
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
