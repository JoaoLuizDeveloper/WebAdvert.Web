using AdvertApi.Models;
using AdvertApi.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebAdvert.Web.ServiceClients
{
    public interface IAdvertApiClients
    {
        Task<CreateAdvertResponse> Create(AdvertDbModel advertmodel);
    }
}
