using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.ViewModels.User
{
    public class GetAllUsersViewModel
    {
        public string Id { get; set; }

        public String UserName { get; set; }
        public String Role { get; set; }
        public String Email { get; set; }
        public DateTime JoinedDate { get; set; }
        public String Status { get; set; }


    }
}
