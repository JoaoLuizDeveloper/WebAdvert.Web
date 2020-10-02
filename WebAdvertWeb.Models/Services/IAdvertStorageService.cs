using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdvertApi.Models.Services
{
    public interface IAdvertStorageService
    {
        Task<string> Add(AdvertModel model);
        Task<bool> Confirm(ConfirmAdvertModel model);
        Task<bool> CheckHealthAsync();
    }
}
