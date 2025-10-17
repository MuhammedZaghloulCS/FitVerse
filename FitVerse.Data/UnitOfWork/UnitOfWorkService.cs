using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.UnitOfWork;
using FitVerse.Data.Service;
using FitVerse.Data.Service.FitVerse.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.UnitOfWork
{
    public class UnitOfWorkService : IUnitOFWorkService
    {
        CoachService coachService;
        ImageHandleService imageHandleService;
        IUnitOfWork unitOfWork;
        IMapper mapper;

        public UnitOfWorkService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public IImageHandleService ImageHandleService { get { return imageHandleService == null ? new ImageHandleService() : imageHandleService; } }
        public ICoachService CoachService { get { return coachService==null?new CoachService(unitOfWork,mapper, ImageHandleService) :coachService; } }
    }
}
