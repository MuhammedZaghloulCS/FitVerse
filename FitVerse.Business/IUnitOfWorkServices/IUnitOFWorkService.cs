using FitVerse.Core.Interfaces;
using FitVerse.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IUnitOfWorkServices
{
    public interface IUnitOFWorkService
    {
        // 🧩 Services
        ICoachService CoachService { get; }
        IImageHandleService ImageHandleService { get; }
        IEquipmentRepository EquipmentRepository { get; }
        IAnatomyRepository AnatomyRepository { get; }
        IMuscleRepository MuscleRepository { get; }
        ICoachRepository CoachRepository { get; }
    }
}
