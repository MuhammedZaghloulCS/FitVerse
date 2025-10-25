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

        public string ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public string CoachId { get; set; }
        public virtual Coach? Coach { get; set; }

        public virtual ICollection<Message>? Messages { get; set; }= new HashSet<Message>();
    }

}
