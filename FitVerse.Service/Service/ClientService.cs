using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.Models;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Profile;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class ClientService: FitVerse.Core.IService.IClientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IImageHandleService imageService;
        private readonly UserManager<ApplicationUser> userManager;

       
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper, IImageHandleService imageService,UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.imageService = imageService;
            this.userManager = userManager;
        }

        public (bool Success, string Message) AddClient(AddClientVM model)
        {

            try
            {
                string? imagePath = null;
                if (model.ClientImageFile != null && model.ClientImageFile.Length > 0)
                {
                    // افترض أن imageService.SaveImage تقبل IFormFile وتعيد مسار الصورة أو null
                    imagePath = imageService.SaveImage(model.ClientImageFile);
                }

                var client = mapper.Map<Client>(model);
                client.UserId = "6A29B02B-7643-48C3-9B47-6ECF12F4B9F9";
                //client.Image = imagePath ?? "/Images/default.jpg";

                unitOfWork.Clients.Add(client);
                unitOfWork.Complete();

                return (true, "Client added successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        public (bool Success, string Message) AddClientByAdmin(Client client)
        {

            try
            {


                unitOfWork.Clients.Add(client);
                unitOfWork.Complete();

                return (true, "Client added successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

    
        public List<ClientDashVM> GetAllClients()
        {
            //Guid coachId = Guid.Parse("11111111-1111-1111-1111-111111111111");// Coach logged in ID

            var clients = unitOfWork.Clients.GetAll();
            return mapper.Map<List<ClientDashVM>>(clients);
        }

        public async Task<(bool Success, string Message)> UpdateClientGoals(string userName, ClientViewModel clientPhysicalInfo)
        {
            var user = await userManager.FindByNameAsync(userName);
           if(user==null)
           {
                return (false, "User Not Found");
           }
            return  unitOfWork.Clients.UpdateClientGoalsRepo(user.Id, clientPhysicalInfo);
        }
    }
}
