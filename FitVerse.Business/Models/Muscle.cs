using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Muscle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public int AnatomyId { get; set; }
        public string ImagePath{get;set;}
        public virtual Anatomy? Anatomy { get; set; }

        public virtual ICollection<Exercise>? Exercises { get; set; } = new HashSet<Exercise>();
    }

}
