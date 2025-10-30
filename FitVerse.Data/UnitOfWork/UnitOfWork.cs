using FitVerse.Core.Interfaces;
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
        DietPlanRepository dietPlans;
        PaymentRepository payments;
        CoachFeedbackRepository coachFeedbacks;
        CoachSpecialtiesRepository coachSpecialties;

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
        public IDietPlanRepository DietPlans
        {
            get
            {
                if (dietPlans == null)
                    dietPlans = new DietPlanRepository(_context);
                return dietPlans;
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
                return clients;

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

        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
