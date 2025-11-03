using AutoMapper;
using FitVerse.Core.Interfaces;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.Models;
using FitVerse.Core.Repositories;
using FitVerse.Core.Services;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.UnitOfWorkServices;
using FitVerse.Data.Context;
using FitVerse.Data.Repositories;
using FitVerse.Data.Service;
using FitVerse.Data.Service.FitVerse.Data.Service;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FitVerse.Data.UnitOfWork
{
    public class UnitOfWorkService :IUnitOFWorkService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly FitVerseDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<AccountService> accountLogger;
        private readonly ILogger<ClientDashboardService> clientDashboardLogger;

        // Lazy-loaded services
        private ICoachService coachService;
        private IImageHandleService imageHandleService;
        private IClientService clientService;
        private IAnatomyService anatomyService;
        //private IUsers users;
        private IEquipmentService equipmentService;
        private IDietPlan dietPlanService;
        private IUsersService users;
        private IAccountService account;
        //private IUsers users;
        private IAdminService adminService;
        private IClientOnCoachesService _clientOnCoachesService;
        private IExercisePlanService exercisePlanService;
        private IExercisePlanDetailService exercisePlanDetailService;
        private IPackageAppService packageAppService;
        private IDailyLogService dailyLogService;
        private IClientDashboardService clientDashboardService;

        // Lazy-loaded repositories
        private IEquipmentRepository equipmentRepository;
        private IAnatomyRepository anatomyRepository;
        private IMuscleRepository muscleRepository;
        private ICoachRepository coachRepository;
        private IClientRepository clientRepository;
        private IPackageRepository packageRepository;
        private IDailyLogRepository dailyLogRepository;
        private IExercisePlanDetailRepository exercisePlanDetailRepository;
        private IDietPlanRepository dietPlanRepository;
        private IExercisePlanRepository exercisePlanRepository;
        private IExerciseRepository exerciseRepository;
        private ICoachPackageRepository coachPackageRepository;
        private ISpecialtiesRepository specialtiesRepository;
        private ICoachSpecialtiesRepository coachSpecialtiesRepository;

        public UnitOfWorkService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, ILogger<AccountService> accountLogger, ILogger<ClientDashboardService> clientDashboardLogger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = ((UnitOfWork)unitOfWork)._context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            this.accountLogger = accountLogger;
            this.clientDashboardLogger = clientDashboardLogger;
        }

        // Identity
        public UserManager<ApplicationUser> UserManager => userManager;

        // Services
        public IImageHandleService ImageHandleService => imageHandleService ??= new ImageHandleService();
        public ICoachService CoachService => coachService ??= new CoachService(unitOfWork, mapper, ImageHandleService);
        public IAnatomyService AnatomyService => anatomyService ??= new AnatomyService(unitOfWork, mapper, ImageHandleService);
        public IClientService ClientService => clientService ??= new ClientService(unitOfWork, mapper, ImageHandleService, userManager);
        public IEquipmentService EquipmentService => equipmentService ??= new EquipmentService(unitOfWork, mapper, ImageHandleService);
        public IDietPlan DietPlanService => dietPlanService ??= new DietPlanService(unitOfWork, mapper);
        public IPackageAppService PackageAppService => packageAppService ??= new PackageAppService(unitOfWork, mapper);
        public IUsersService UsersService => users ??= new UsersService(userManager, mapper, unitOfWork);
        public IAccountService AccountService => account ??= new AccountService(userManager, mapper, signInManager, ClientService, accountLogger);
        public IAdminService AdminService => adminService ??= new AdminService(unitOfWork, userManager);
        public IDailyLogService DailyLogService => dailyLogService ??= new DailyLogService(unitOfWork);
        public IClientDashboardService ClientDashboardService => clientDashboardService ??= new ClientDashboardService(unitOfWork, ClientService, CoachService, ImageHandleService, httpContextAccessor, clientDashboardLogger);
        public IClientOnCoachesService clientOnCoachesService => _clientOnCoachesService ??= new ClientOnCoachesService(unitOfWork);
        public IExercisePlanService ExercisePlanService => exercisePlanService ??= new ExercisePlanService(unitOfWork,mapper);
        public IExercisePlanDetailService ExercisePlanDetailService => exercisePlanDetailService ??= new ExercisePlanDetailService(unitOfWork);



        // Repositories
        public IEquipmentRepository EquipmentRepository => equipmentRepository ??= new EquipmentRepository(context);
        public IAnatomyRepository AnatomyRepository => anatomyRepository ??= new AnatomyRepository(context);
        public IMuscleRepository MuscleRepository => muscleRepository ??= new MuscleRepository(context);
        public ICoachRepository CoachRepository => coachRepository ??= new CoachRepository(context);
        public IClientRepository ClientRepository => clientRepository ??= new ClientRepository(context);
        public IPackageRepository PackageRepository => packageRepository ??= new PackageRepository(context);
        public IDailyLogRepository DailyLogRepository => dailyLogRepository ??= new DailyLogRepository(context);
        public IExercisePlanDetailRepository ExercisePlanDetailRepository => exercisePlanDetailRepository ??= new ExercisePlanDetailRepository(context);
        public IDietPlanRepository DietPlanRepository => dietPlanRepository ??= new DietPlanRepository(context);
        public IExerciseRepository ExerciseRepository => new ExersiceRepository(context);
        public IExercisePlanRepository ExercisePlanRepository => exercisePlanRepository ??= new ExercisePlanRepository(context);
        public ICoachPackageRepository CoachPackageRepository => new CoachPackageRepository(context);
        public ISpecialtiesRepository SpecialtiesRepository => new SpecialityRepository(context);
        public ICoachSpecialtiesRepository CoachSpecialtiesRepository => new CoachSpecialtiesRepository(context);
    }
}
