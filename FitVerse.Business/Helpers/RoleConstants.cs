using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.Helpers
{
    public static class RoleConstants
    {
        public const string Admin = "Admin";
        public const string Coach = "Coach";
        public const string Client = "Client";

        public static readonly string[] AllRoles = { Admin, Coach, Client };

        public static class Claims
        {
            public const string CanManageUsers = "CanManageUsers";
            public const string CanManageCoaches = "CanManageCoaches";
            public const string CanManageClients = "CanManageClients";
            public const string CanCreateExercisePlans = "CanCreateExercisePlans";
            public const string CanCreateDietPlans = "CanCreateDietPlans";
            public const string CanViewReports = "CanViewReports";
        }
    }
}
