using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Coach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface ICoachService:IService
    {
        (bool Success, string Message) AddCoach(AddCoachVM model); //برتجع تابل tuble    }

    }
}
