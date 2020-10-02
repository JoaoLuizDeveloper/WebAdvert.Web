using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace AdvertApi.Models.Services
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertModel, AdvertDbModel>();
        }
    }
}
