using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class ExercisePlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int DurationInDays { get; set; }
        public string? Notes { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client? Client { get; set; }
        public Guid CoachId { get; set; }
        public virtual Coach? Coach { get; set; }
        public virtual ICollection<ExercisePlanDetail>? ExercisePlanDetails { get; set; }= new HashSet<ExercisePlanDetail>();
    }

}
