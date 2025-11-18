using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.ServiceData;

namespace MyServicesTelegramBotBussinessTier.Objects.Service.Services
{
    public class clsSaveServiceService : ISaveService<bool,clsService>
    {
        public Exception Exception;
        public bool Save(clsService Service)
        {

            switch (Service.SaveMode)
            {
                case DataTypes.clsModes.enSaveMode.eAdd:
                    {
                        Service.ServiceID = clsServiceData.AddService(Service.ServiceDTO, ref Exception);

                        if(Service.ServiceID != null)
                        {
                            Service.SaveMode = DataTypes.clsModes.enSaveMode.eUpdate;
                            return true;
                        }

                        return false;
                    }

                case DataTypes.clsModes.enSaveMode.eUpdate:
                    {
                        return clsServiceData.UpdateService(Service.UpdateServiceDTO, ref Exception);
                    }
            }
            return false;

        }

    }
}
