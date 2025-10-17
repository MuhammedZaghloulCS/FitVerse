using AutoMapper;
using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels.Anatomy;


using FitVerse.Core.ViewModels.Package;
using FitVerse.Data.Models;


namespace FitVerse.Core.MapperConfigs
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Anatomy,AnatomyVM>().ReverseMap();
            CreateMap<Equipment,EquipmentVM>().ReverseMap();
            CreateMap<AddAnatomyVM, Anatomy>().ReverseMap();
            CreateMap <Package,PackageVM>().ReverseMap();
            CreateMap<AddPackageVM, Package>().ReverseMap();
           // CreateMap<Exercise, ExersiceVM>().ReverseMap();
          //  CreateMap<AddExersiceVM, Exercise>().ReverseMap();

        }

    }
}
