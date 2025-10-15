using FitVerse.Core.Interfaces;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Context;
using FitVerse.Data.Repositories;

namespace FitVerse.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitVerseDbContext _context;
        AnatomyRepositroy anatomies;
        EquipmentRepository equipments;
        MuscleRepository muscles;
       
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

         
            //Coaches = coaches;
            //Clients = clients;
           
            //Messages = messages;
            //Chats = chats;
            //DietPlans = dietPlans;
            //Exercises = exercises; 
            //Packages = packages;
            //Payments = payments;
            //Notifications = notifications;
            //CoachSpecialties = coachSpecialties;
            //CoachFeedbacks = coachFeedbacks;
            
            //ExercisePlans = exercisePlans;
            //ExercisePlanDetails = exercisePlanDetails;
            //Specialties = specialties;
        }

        public IMuscleRepository Muscles
        {
            get
            {
                if (muscles == null)
                muscles=new MuscleRepository(_context);
                return muscles;
            }
        }
        //public ICoachRepository Coaches { get; }
        //public IClientRepository Clients { get; }
        public IAnatomyRepository Anatomies
        {
            get
            {
                if (anatomies == null)
                    anatomies = new AnatomyRepositroy(_context);
                return anatomies;
            }
        }
        //public IMessageRepository Messages { get; }
        //public IChatRepository Chats { get; }
        //public IDietPlanRepository DietPlans { get; }
        //public IExerciseRepository Exercises { get; }
        //public IPackageRepository Packages { get; }
        //public IPaymentRepository Payments { get; }
        //public INotificationRepository Notifications { get; }
        //public ICoachSpecialtiesRepository CoachSpecialties { get; }
        //public ICoachFeedbackRepository CoachFeedbacks { get; }
        public IEquipmentRepository Equipments
        {
            get
            {
                if (equipments == null)
                    equipments = new EquipmentRepository(_context);
                return equipments;
            }
        }
        //public IExercisePlanRepository ExercisePlans { get; }
        //public IExercisePlanDetailRepository ExercisePlanDetails { get; }
        //public ISpecialtiesRepository Specialties { get; }
        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
