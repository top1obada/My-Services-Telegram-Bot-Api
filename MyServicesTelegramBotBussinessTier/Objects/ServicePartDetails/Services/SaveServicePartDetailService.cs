using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.ServicePartDetailsData;
using MyServicesTelegramBotBussinessTier.Objects.ServicePartDetails;

namespace MyServicesTelegramBotBussinessTier.Objects.ServicePartDetails.Services
{
    public class clsSaveServicePartDetailsService : ISaveService<bool, clsServicePartDetail>
    {
        public Exception Exception;
        public bool Save(clsServicePartDetail ServicePartDetails)
        {
            switch (ServicePartDetails.SaveMode)
            {
                case DataTypes.clsModes.enSaveMode.eAdd:
                    {
                        ServicePartDetails.ServicePartDetailsID = clsServicePartDetailsData.AddServicePartDetails(ServicePartDetails.ServicePartDetailsDTO, ref Exception);

                        if (ServicePartDetails.ServicePartDetailsID != null)
                        {
                            ServicePartDetails.SaveMode = DataTypes.clsModes.enSaveMode.eUpdate;
                            return true;
                        }

                        return false;
                    }

                case DataTypes.clsModes.enSaveMode.eUpdate:
                    {
                        return clsServicePartDetailsData.UpdateServicePartDetails(ServicePartDetails.UpdateServicePartDetailsDTO, ref Exception);
                    }
            }
            return false;
        }
    }
}