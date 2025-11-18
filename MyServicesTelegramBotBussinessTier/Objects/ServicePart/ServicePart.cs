using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.DataTypes;
using MyServicesTelegramBotDataTier.Data.ServicePartData;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDTO;

namespace MyServicesTelegramBotBussinessTier.Objects.ServicePart
{
    public class clsServicePart
    {
        public int? ServicePartID { get;internal set; }
        public int? ServiceID { get; set; }
        public string? ServicePartName { get; set; }
        public clsModes.enSaveMode SaveMode { get; internal set; }

        public clsServicePartDTO ServicePartDTO
        {
            get
            {
                return new clsServicePartDTO()
                {
                    ServicePartID = this.ServicePartID,
                    ServiceID = this.ServiceID,
                    ServicePartName = this.ServicePartName
                };
            }
            set
            {
                ServiceID = value.ServiceID;
                ServicePartName = value.ServicePartName;
            }
        }

        public clsUpdateServicePartDTO UpdateServicePartDTO
        {
            get
            {
                return new clsUpdateServicePartDTO()
                {
                    ServicePartID = this.ServicePartID,           
                    ServicePartName = this.ServicePartName
                };
            }
            set
            {            
                ServicePartName = value.ServicePartName;
            }
        }

        public clsServicePart()
        {
            SaveMode = clsModes.enSaveMode.eAdd;
        }

        private clsServicePart(clsServicePartDTO ServicePartDTO)
        {
            this.ServicePartID = ServicePartDTO.ServicePartID;
            this.ServicePartDTO = ServicePartDTO;
            SaveMode = clsModes.enSaveMode.eUpdate;
        }

        public static clsServicePart Find(int servicePartID, ref Exception Exception)
        {
            var Result = clsServicePartData.FindServicePart(servicePartID, ref Exception);

            if (Result != null) return new clsServicePart(Result);

            return null;
        }
    }
}