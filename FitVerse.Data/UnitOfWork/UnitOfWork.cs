using FitVerse.Core.Interfaces;
using FitVerse.Core.Repositories;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Context;
using FitVerse.Data.Models;
using FitVerse.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitVerse.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly FitVerseDbContext _context;
        AnatomyRepository anatomies;
        EquipmentRepository equipments;
        MuscleRepository muscles;
        PackageRepository package;
        ExersiceRepository exersice;
        CoachRepository coaches;
        ClientRepository clients;
        SpecialityRepository specialties;
        PaymentRepository payments;
        CoachFeedbackRepository coachFeedbacks;
        CoachSpecialtiesRepository coachSpecialties;
        CoachPackageRepository coachPackageRepository;
        IDailyLogRepository dailyLogRepository;
        DietPlanRepository dietPlanRepository;
        ExercisePlanDetailRepository ExercisePlanDetail;





        public UnitOfWork(FitVerseDbContext context)
        {
            _context = context;
        }

        public IMuscleRepository Muscles
        {
            get
            {
                if (muscles == null)
                    muscles = new MuscleRepository(_context);
                return muscles;
            }
        }
        public IAnatomyRepository Anatomies
        {
            get
            {
                if (anatomies == null)
                    anatomies = new AnatomyRepository(_context);
                return anatomies;
            }
        }
        public IEquipmentRepository Equipments
        {
            get
            {
                if (equipments == null)
                    equipments = new EquipmentRepository(_context);
                return equipments;
            }
        }

        public IPackageRepository Packages
        {
            get
            {
                if (package == null)
                    package = new PackageRepository(_context);
                return package;
            }
        }
        public IClientRepository Clients
        {
            get
            {
                if (clients == null)
                    clients = new ClientRepository(_context);
                return clients; // <--- صحح هنا
            }
        }
        public IExerciseRepository Exercises
        {
            get
            {
                if (exersice == null)
                    exersice = new ExersiceRepository(_context);
                return exersice;
            }
        }

        public ICoachRepository Coaches
        {
            get
            {
                if (coaches == null)
                    coaches = new CoachRepository(_context);
                return coaches;
            }
        }

        public ISpecialtiesRepository Specialties { get
            {
                if (specialties == null)
                    specialties = new SpecialityRepository(_context);
                return specialties;
            }
        }

        public IPaymentRepository Payments {
            get
            {
                if (payments == null)
                    payments = new PaymentRepository(_context);
                return payments;
            }

        }
        public ICoachSpecialtiesRepository CoachSpecialties
        {
            get
            {
                if (coachSpecialties == null)
                    coachSpecialties = new CoachSpecialtiesRepository(_context);
                return coachSpecialties;
            }
        }


        public ICoachFeedbackRepository CoachFeedbacks
        {
            get
            {
                if (coachFeedbacks == null)
                    coachFeedbacks = new CoachFeedbackRepository(_context);
                return coachFeedbacks;
            }

        }

      
        ICoachPackageRepository IUnitOfWork.coachPackageRepository
        {
            get
            {
                if (coachPackageRepository == null)
                    coachPackageRepository = new CoachPackageRepository(_context);
                return coachPackageRepository;
            }


        }

        public IDietPlanRepository DietPlans {
            get
            {
                if (dietPlanRepository == null)
                    dietPlanRepository = new DietPlanRepository(_context);
                return dietPlanRepository;
            }

        }
        public IExercisePlanDetailRepository ExercisePlanDetails { 
            get
            {
                if (ExercisePlanDetail == null)
                    ExercisePlanDetail = new ExercisePlanDetailRepository(_context);
                return ExercisePlanDetail;
            }
        }





        IDailyLogRepository IUnitOfWork.DailyLogsRepository
        {
            get
            {
                if (dailyLogRepository == null)
                    dailyLogRepository = new DailyLogRepository(_context);
                return dailyLogRepository;
            }
        }

        public IExercisePlanRepository ExercisePlans => new ExercisePlanRepository(_context);


        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
