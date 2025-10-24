using FitVerse.Core.ViewModels.Admin;
using FitVerse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IAdminService
    {
        public int getTotalUsersCount();
        public int getCoachesCount();
        public int getSoldedPackagesCount();
        public decimal getTotalRevenue();
        public List<TopCoachViewModel> getTopRatedCoaches();

    }
}
