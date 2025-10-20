using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Data.Models;
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

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper, IImageHandleService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.imageService = imageService;
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
                client.Id = Guid.NewGuid();
                client.UserId = Guid.Parse("6A29B02B-7643-48C3-9B47-6ECF12F4B9F9");
                client.Image = imagePath ?? "/Images/default.jpg";

                unitOfWork.Clients.Add(client);
                unitOfWork.Complete();

                return (true, "Client added successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public List<AddClientVM> GetAllClients()
        {
            var clients = unitOfWork.Clients.GetAll();
            return mapper.Map<List<AddClientVM>>(clients);
        }
    

}
}
