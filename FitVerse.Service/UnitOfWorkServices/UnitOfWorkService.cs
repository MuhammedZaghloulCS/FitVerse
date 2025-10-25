using AutoMapper;
using FitVerse.Core.Interfaces;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.Models;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Context;
using FitVerse.Data.Repositories;
using FitVerse.Data.Service;
using FitVerse.Data.Service.FitVerse.Data.Service;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Identity;

namespace FitVerse.Data.UnitOfWork
{
    public class UnitOfWorkService : IUnitOFWorkService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly FitVerseDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;



        // Lazy-loaded services
        private ICoachService coachService;
        private IImageHandleService imageHandleService;
        private IClientService clientService;
        private IUsersService users;
        private IAccountService account;

        // Lazy-loaded repositories
        private IEquipmentRepository equipmentRepository;
        private IAnatomyRepository anatomyRepository;
        private IMuscleRepository muscleRepository;
        private ICoachRepository coachRepository;
        private IClientRepository clientRepository;


        public UnitOfWorkService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = ((UnitOfWork)unitOfWork)._context; 
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //Identity
        public UserManager<ApplicationUser> UserManager => userManager;


        // Services
        public IImageHandleService ImageHandleService => imageHandleService ??= new ImageHandleService();
        public ICoachService CoachService => coachService ??= new CoachService(unitOfWork, mapper, ImageHandleService);
        public IClientService ClientService => clientService ??= new ClientService(unitOfWork, mapper, ImageHandleService);
        public IUsersService UsersService => users ??= new UsersService(userManager, mapper);
        public IAccountService AccountService => account ??= new AccountService(userManager, mapper,signInManager);

        // Repositories
        public IEquipmentRepository EquipmentRepository => equipmentRepository ??= new EquipmentRepository(context);
        public IAnatomyRepository AnatomyRepository => anatomyRepository ??= new AnatomyRepository(context);
        public IMuscleRepository MuscleRepository => muscleRepository ??= new MuscleRepository(context);
        public ICoachRepository CoachRepository => coachRepository ??= new CoachRepository(context);
        public IClientRepository ClientRepository => clientRepository ??= new ClientRepository(context);

        public ISpecialtiesRepository SpecialtiesRepository => throw new NotImplementedException();

        public ICoachSpecialtiesRepository CoachSpecialtiesRepository => throw new NotImplementedException();
    }
}
