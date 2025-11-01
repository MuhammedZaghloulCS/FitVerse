using AutoMapper;
using FitVerse.Core.Models;
using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels;
using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.ExerciseVM;
using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Core.ViewModels.Package;
using FitVerse.Core.ViewModels.Plan;
using FitVerse.Core.ViewModels.User;
using FitVerse.Data.Models;
using FitVerse.Core.ViewModels;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.ExerciseVM;
using FitVerse.Core.ViewModels.User;
using FitVerse.Core.Models;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Core.ViewModels.DietPlan;


namespace FitVerse.Core.MapperConfigs
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Anatomy,AnatomyVM>().ReverseMap();
            CreateMap<Equipment,EquipmentVM>().ReverseMap();
            CreateMap<AddAnatomyVM, Anatomy>().ReverseMap();
            CreateMap<Anatomy, AddAnatomyVM>()
.ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image ?? "/Images/default.jpg"));
            CreateMap<AddEquipmentVM, Equipment>().ReverseMap();
            CreateMap<Equipment, AddEquipmentVM>()
     .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image ?? "/Images/default.jpg"));
           
            CreateMap<Coach,AddCoachVM>().ReverseMap();
            CreateMap <Package,PackageVM>().ReverseMap();
            CreateMap<Muscle, MuscleVM>().ReverseMap();
            CreateMap<Muscle, AddMuscleVM>().ReverseMap();
            CreateMap<AddPackageVM, Package>().ReverseMap();
            CreateMap<AddMuscleVM, MuscleVM>().ReverseMap();
            CreateMap<Exercise, ExerciseVM>().
                ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(s => s.Equipment.Name)).
                ForMember(dest => dest.MuscleName, opt => opt.MapFrom(s => s.Muscle.Name))
                .ReverseMap();
            CreateMap<AddExerciseVM, Exercise>().ReverseMap();
            CreateMap<AddClientVM, Client>().ReverseMap();
            CreateMap<Client, ClientDashVM>().ReverseMap();
            CreateMap<ApplicationUser, GetAllUsersViewModel>().ReverseMap();
            CreateMap<DietPlan, DietPlanVM>().ReverseMap();


            CreateMap<Client, ClientsVM>().ReverseMap();
            CreateMap<ExercisePlan, ExercisePlanVM>().ReverseMap();
            CreateMap<ExercisePlanDetail, ExercisePlanDetailVM>().ReverseMap();
        }

    }
}
