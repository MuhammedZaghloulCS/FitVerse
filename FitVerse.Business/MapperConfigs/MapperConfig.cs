using AutoMapper;
using FitVerse.Core.viewModels;

using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Meuscle;
using FitVerse.Core.ViewModels.Package;

using FitVerse.Data.Models;
using FitVerse.Core.ViewModels;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.ExerciseVM;


namespace FitVerse.Core.MapperConfigs
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Anatomy,AnatomyVM>().ReverseMap();
            CreateMap<Equipment,EquipmentVM>().ReverseMap();
            CreateMap<AddAnatomyVM, Anatomy>().ReverseMap();
            CreateMap<AddAnatomyVM, Equipment>().ReverseMap();
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
            // CreateMap<Exercise, ExersiceVM>().ReverseMap();
            //  CreateMap<AddExersiceVM, Exercise>().ReverseMap();

        }

    }
}
