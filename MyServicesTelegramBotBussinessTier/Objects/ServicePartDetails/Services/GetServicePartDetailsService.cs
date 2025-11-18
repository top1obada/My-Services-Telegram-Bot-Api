using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.ServicePartDetailsData;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDetailsDTO;

namespace MyServicesTelegramBotBussinessTier.Objects.ServicePartDetails.Services
{
    public class clsGetServicePartDetailsService 
    {
        public Exception Exception;
        public clsServicePartDetailDTO Get(int ServicePartDetailsID)
        {
            return clsServicePartDetailsData.GetServicePartDetails(ServicePartDetailsID, ref Exception);
        }
    }
}