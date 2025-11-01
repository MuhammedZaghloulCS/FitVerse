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
        private IAdminService adminService;

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
        public IImageHandleService ImageHandleService
        {
            get
            {
                if (imageHandleService == null)
                    imageHandleService = new ImageHandleService();
                return imageHandleService;
            }
        }

        public ICoachService CoachService
        {
            get
            {
                if (coachService == null)
                    coachService = new CoachService(unitOfWork, mapper, ImageHandleService);
                return coachService;
            }
        }

        public IClientService ClientService
        {
            get
            {
                if (clientService == null)
                    clientService = new ClientService(unitOfWork, mapper, ImageHandleService,userManager);
                return clientService;
            }
        }

        public IUsersService UsersService
        {
            get
            {
                if (users == null)
                    users = new UsersService(userManager, mapper, unitOfWork);
                return users;
            }
        }

        public IAccountService AccountService
        {
            get
            {
                if (account == null)
                    account = new AccountService(userManager, mapper, signInManager);
                return account;
            }
        }

        public IAdminService AdminService
        {
            get
            {
                if (adminService == null)
                    adminService = new AdminService(unitOfWork, userManager);
                return adminService;
            }
        }

        // Repositories
        public IEquipmentRepository EquipmentRepository
        {
            get
            {
                if (equipmentRepository == null)
                    equipmentRepository = new EquipmentRepository(context);
                return equipmentRepository;
            }
        }

        public IAnatomyRepository AnatomyRepository
        {
            get
            {
                if (anatomyRepository == null)
                    anatomyRepository = new AnatomyRepository(context);
                return anatomyRepository;
            }
        }

        public IMuscleRepository MuscleRepository
        {
            get
            {
                if (muscleRepository == null)
                    muscleRepository = new MuscleRepository(context);
                return muscleRepository;
            }
        }

        public ICoachRepository CoachRepository
        {
            get
            {
                if (coachRepository == null)
                    coachRepository = new CoachRepository(context);
                return coachRepository;
            }
        }

        public IClientRepository ClientRepository
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(context);
                return clientRepository;
            }
        }

        public ISpecialtiesRepository SpecialtiesRepository => throw new NotImplementedException();

        public ICoachSpecialtiesRepository CoachSpecialtiesRepository => throw new NotImplementedException();
    }
}
