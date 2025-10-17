using FitVerse.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.UnitOfWork
{
    public interface IUnitOFWorkService
    {
        ICoachService CoachService { get; }
        IImageHandleService ImageHandleService { get; }
    }
}
