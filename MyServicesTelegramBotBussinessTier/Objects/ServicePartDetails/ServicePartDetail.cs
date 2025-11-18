using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.DataTypes;
using MyServicesTelegramBotDataTier.Data.ServicePartDetailsData;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDetailsDTO;

namespace MyServicesTelegramBotBussinessTier.Objects.ServicePartDetails
{
    public class clsServicePartDetail
    {
        public int? ServicePartDetailsID { get;internal set; }
        public int? ServicePartID { get; set; }
        public string? Title { get; set; }
        public short? WorkTimePerDays { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string? Text_Description { get; set; }
        public clsModes.enSaveMode SaveMode { get; internal set; }

        public clsServicePartDetailDTO ServicePartDetailsDTO
        {
            get
            {
                return new clsServicePartDetailDTO()
                {
                    ServicePartDetailsID = this.ServicePartDetailsID,
                    ServicePartID = this.ServicePartID,
                    Title = this.Title,
                    WorkTimePerDays = this.WorkTimePerDays,
                    MinPrice = this.MinPrice,
                    MaxPrice = this.MaxPrice,
                    Text_Description = this.Text_Description
                };
            }
            set
            {
                ServicePartID = value.ServicePartID;
                Title = value.Title;
                WorkTimePerDays = value.WorkTimePerDays;
                MinPrice = value.MinPrice;
                MaxPrice = value.MaxPrice;
                Text_Description = value.Text_Description;
            }
        }

        public clsUpdateServicePartDetailDTO UpdateServicePartDetailsDTO
        {
            get
            {
                return new clsUpdateServicePartDetailDTO()
                {
                    ServicePartDetailsID = this.ServicePartDetailsID,
                    Title = this.Title,
                    WorkTimePerDays = this.WorkTimePerDays,
                    MinPrice = this.MinPrice,
                    MaxPrice = this.MaxPrice,
                    Text_Description = this.Text_Description
                };
            }
            set
            {
                Title = value.Title;
                WorkTimePerDays = value.WorkTimePerDays;
                MinPrice = value.MinPrice;
                MaxPrice = value.MaxPrice;
                Text_Description = value.Text_Description;
            }
        }

        public clsServicePartDetail()
        {
            SaveMode = clsModes.enSaveMode.eAdd;
        }

        private clsServicePartDetail(clsServicePartDetailDTO ServicePartDetailsDTO)
        {
            this.ServicePartDetailsID = ServicePartDetailsDTO.ServicePartDetailsID;
            this.ServicePartDetailsDTO = ServicePartDetailsDTO;
            SaveMode = clsModes.enSaveMode.eUpdate;
        }


        public static clsServicePartDetail Find(int servicePartDetailsID, ref Exception Exception)
        {
            var Result = clsServicePartDetailsData.FindServicePartDetails(servicePartDetailsID, ref Exception);

            if (Result != null) return new clsServicePartDetail(Result);

            return null;
        }
    }
}