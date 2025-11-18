using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.ServicePartData;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDTO;

namespace MyServicesTelegramBotBussinessTier.Objects.ServicePart.Services
{
    public class clsGetAllServicePartsService : IGetAllServiceParameter<clsServicePartDTO,int>
    {
        public Exception exception;
        public List<clsServicePartDTO> Get(int ServiceID)
        {

            return clsServicePartData.GetServiceParts(ServiceID, ref exception);

        }

    }
}
