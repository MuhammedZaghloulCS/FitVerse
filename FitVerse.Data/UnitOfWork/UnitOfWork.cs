using FitVerse.Core.Interfaces;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Context;
using FitVerse.Data.Models;
using FitVerse.Data.Repositories;

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
        SpecialityRepository specialties;

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
                if(coaches==null)
                    coaches=new CoachRepository(_context);
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

        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
