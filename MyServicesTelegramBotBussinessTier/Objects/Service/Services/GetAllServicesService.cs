using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.ServiceData;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicesDTO;

namespace MyServicesTelegramBotBussinessTier.Objects.Service.Services
{
    public class clsGetAllServicesService : IGetAllServiceParameter<clsUpdateServiceDTO,long>
    {
        public Exception Exception;
        public List<clsUpdateServiceDTO> Get(long BotID)
        {

            return clsServiceData.GetAllServices(BotID,ref Exception);

        }

    }
}
