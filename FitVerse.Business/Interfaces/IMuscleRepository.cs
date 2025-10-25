using System;
using FitVerse.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.Interfaces
{
    public interface IMuscleRepository: IGenericRepository<Muscle>
    {
        IEnumerable<Muscle> GetAllWithAnatomy();
        Muscle GetByIdWithAnatomy(int id);
    }
}
