using FitVerse.Core.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.IService
{
    public interface IClientService :IService
    {
        (bool Success, string Message) AddClient(AddClientVM model);
        //(bool Success, string Message) UpdateClient(AddClientVM model);
        //(bool Success, string Message) DeleteClient(Guid id);
        //AddClientVM GetClientById(Guid id);
        List<ClientDashVM> GetAllClients();
    }
}
