using AutoMapper;
using FitVerse.Core.ViewModels.Anatomy;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Core.ViewModels.Package;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

    }
}
