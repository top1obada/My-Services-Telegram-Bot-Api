using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.ServicePartDetailsData;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDTO;

namespace MyServicesTelegramBotDataTier.Data.ServicePartDetails.Services
{
    public class clsGetAllServicePartDetailsTitlesService : IGetAllServiceParameter<clsServicePartDetailsTitleDTO, int>
    {
        public Exception exception;

        public List<clsServicePartDetailsTitleDTO> Get(int ServicePartID)
        {
            return clsServicePartDetailsData.GetServicePartDetailsTitles(ServicePartID, ref exception);
        }
    }
}