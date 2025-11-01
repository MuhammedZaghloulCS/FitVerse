using FitVerse.Core.viewModels;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IEquipmentService: IService
    {
        (bool Success, string Message) AddEquipment(AddEquipmentVM model); //برتجع تابل tuble    }
        List<AddEquipmentVM> GetAll(string? search);
        public AddEquipmentVM GetById(int id);
        (bool Success, string Message) Delete(int id);
        (bool Success, string Message) Update(AddEquipmentVM model);
        public (List<AddEquipmentVM> Data, int TotalItems) GetPagedEquipments(int page, int pageSize, string? search);
        public int GetTotalEquipmentCount();
        public int GetTotalExerciseCount();



    }
}
