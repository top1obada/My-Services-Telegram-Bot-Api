using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using MyServicesTelegramBotBussinessTier.DataTypes;
using MyServicesTelegramBotDataTier.Data.ServiceData;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicesDTO;

namespace MyServicesTelegramBotBussinessTier.Objects.Service
{
    public class clsService
    {

        public int? ServiceID { get;internal set; }

        public string? ServiceName { get; set; }

        public int? BotID { get; set; }

        public clsModes.enSaveMode SaveMode { get; internal set; }

        public clsServiceDTO ServiceDTO
        {
            get
            {
                return new clsServiceDTO()
                { ServiceID = this.ServiceID, ServiceName = this.ServiceName, BotID = this.BotID };
            }

            set
            {
                ServiceName = value.ServiceName;
                BotID = value.BotID;
            }

        }


        public clsUpdateServiceDTO UpdateServiceDTO
        {
            get { return new clsUpdateServiceDTO() { ServiceID = this.ServiceID, ServiceName = this.ServiceName }; }

            set
            {
                ServiceName = value.ServiceName;
            }
        }


        public clsService()
        {
            SaveMode = clsModes.enSaveMode.eAdd;
        }


        private clsService(clsServiceDTO ServiceDTO)
        {
            this.ServiceID = ServiceDTO.ServiceID;
            this.ServiceDTO = ServiceDTO;
            SaveMode = clsModes.enSaveMode.eUpdate;
        }


        public static clsService Find(int serviceID, ref Exception Exception)
        { 
            var Result = clsServiceData.FindService(serviceID, ref Exception);

            if (Result != null) return new clsService(Result);

            return null;

        }

       

    }
}
