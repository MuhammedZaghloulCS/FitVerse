using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public Guid ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public Guid CoachId { get; set; }
        public virtual Coach? Coach { get; set; }

        public virtual ICollection<Message>? Messages { get; set; }= new HashSet<Message>();
    }

}
