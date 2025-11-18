using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.ServicePartData;
using MyServicesTelegramBotBussinessTier.Objects.ServicePart;

namespace MyServicesTelegramBotBussinessTier.Objects.ServicePart.Services
{
    public class clsSaveServicePartService : ISaveService<bool, clsServicePart>
    {
        public Exception Exception;
        public bool Save(clsServicePart ServicePart)
        {
            switch (ServicePart.SaveMode)
            {
                case DataTypes.clsModes.enSaveMode.eAdd:
                    {
                        ServicePart.ServicePartID = clsServicePartData.AddServicePart(ServicePart.ServicePartDTO, ref Exception);

                        if (ServicePart.ServicePartID != null)
                        {
                            ServicePart.SaveMode = DataTypes.clsModes.enSaveMode.eUpdate;
                            return true;
                        }

                        return false;
                    }

                case DataTypes.clsModes.enSaveMode.eUpdate:
                    {
                        return clsServicePartData.UpdateServicePart(ServicePart.UpdateServicePartDTO, ref Exception);
                    }
            }
            return false;
        }
    }
}