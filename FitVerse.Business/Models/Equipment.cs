using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; } = null!;
        public virtual ICollection<Exercise>? Exercises { get; set; } = new HashSet<Exercise>();
    }

}
