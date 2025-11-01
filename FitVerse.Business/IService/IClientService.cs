using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Profile;
using FitVerse.Data.Models;
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
        (bool Success, string Message) AddClientByAdmin(Client model);
        //(bool Success, string Message) UpdateClient(AddClientVM model);
        //(bool Success, string Message) DeleteClient(Guid id);
        //AddClientVM GetClientById(Guid id);
        List<ClientDashVM> GetAllClients();
        Task<(bool Success, string Message)> UpdateClientGoals(string userName, ClientViewModel clientPhysicalInfo);
       
    }
}
