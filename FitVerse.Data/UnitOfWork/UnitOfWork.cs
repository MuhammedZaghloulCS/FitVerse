using FitVerse.Core.Interfaces;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Context;
using FitVerse.Data.Repositories;

namespace FitVerse.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitVerseDbContext _context;

        public UnitOfWork(FitVerseDbContext context)
        {
            _context = context;

            Muscles = new MuscleRepository(_context);
            Anatomies = new AnatomyRepository(_context);
            Packages = new PackageRepository(_context);
            Equipments = new EquipmentRepository(_context);
            Coaches=new CoachRepository(_context);
        }

        public IMuscleRepository Muscles { get; }
        public IAnatomyRepository Anatomies { get; }
        public IPackageRepository Packages { get; }
        public IEquipmentRepository Equipments { get; }

        public ICoachRepository Coaches { get; }

        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
