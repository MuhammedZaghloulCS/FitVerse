using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class CoachFeedback
    {
        public int Id { get; set; }
        public DateTime FeedbackDate { get; set; } = DateTime.Now;
               
        public string Comments { get; set; }              
        public int Rate{ get; set; }


        public Guid ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public Guid CoachId { get; set; }
        public virtual Coach? Coach { get; set; }


    }

}
