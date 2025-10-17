using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.Interfaces
{
    public interface ICoachRepository : IGenericRepository<Data.Models.Coach>
    {
        Data.Models.Coach GetCoachByIdGuid(Guid id);
        (bool Success, string Message) DeleteCoachById(Guid id);


    }
}

    
