using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.DataTypes;
using MyServicesTelegramBotDataTier.Data.ImageData;
using MyServicesTelegramBotDTO.ObjectsDTO.ImageDTO;

namespace MyServicesTelegramBotBussinessTier.Objects.Image
{
    public class clsImage
    {
        public int? ImageID { get;internal set; }
        public int? ServicePartDetailID { get; set; }
        public string? ImagePath { get; set; }
        public clsModes.enSaveMode SaveMode { get; internal set; }

        public clsImageDTO ImageDTO
        {
            get
            {
                return new clsImageDTO()
                {
                    ImageID = this.ImageID,
                    ServicePartDetailID = this.ServicePartDetailID,
                    ImagePath = this.ImagePath
                };
            }
            set
            {
                ServicePartDetailID = value.ServicePartDetailID;
                ImagePath = value.ImagePath;
            }
        }

        public clsUpdateImageDTO UpdateImageDTO
        {
            get
            {
                return new clsUpdateImageDTO()
                {
                    ImageID = this.ImageID,
                   
                    ImagePath = this.ImagePath
                };
            }
            set
            {
                
                ImagePath = value.ImagePath;
            }
        }

        public clsImage()
        {
            SaveMode = clsModes.enSaveMode.eAdd;
        }

        private clsImage(clsImageDTO ImageDTO)
        {
            this.ImageID = ImageDTO.ImageID;
            this.ImageDTO = ImageDTO;
            SaveMode = clsModes.enSaveMode.eUpdate;
        }

        public static clsImage Find(int imageID, ref Exception Exception)
        {
            var Result = clsImageData.FindImage(imageID, ref Exception);

            if (Result != null) return new clsImage(Result);

            return null;
        }
    }
}