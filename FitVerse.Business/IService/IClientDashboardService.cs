using FitVerse.Core.ViewModels.ClientDashboard;
using System.Threading.Tasks;

namespace FitVerse.Core.IUnitOfWorkServices
{
    public interface IClientDashboardService
    {
       ClientDashboardViewModel GetClientDashboard();
    }
}
