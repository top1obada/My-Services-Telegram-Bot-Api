using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.ImageData;
using MyServicesTelegramBotBussinessTier.Objects.Image;

namespace MyServicesTelegramBotBussinessTier.Objects.Image.Services
{
    public class clsSaveImageService : ISaveService<bool, clsImage>
    {
        public Exception Exception;
        public bool Save(clsImage Image)
        {
            switch (Image.SaveMode)
            {
                case DataTypes.clsModes.enSaveMode.eAdd:
                    {
                        Image.ImageID = clsImageData.AddImage(Image.ImageDTO, ref Exception);

                        if (Image.ImageID != null)
                        {
                            Image.SaveMode = DataTypes.clsModes.enSaveMode.eUpdate;
                            return true;
                        }

                        return false;
                    }

                case DataTypes.clsModes.enSaveMode.eUpdate:
                    {
                        return clsImageData.UpdateImage(Image.UpdateImageDTO, ref Exception);
                    }
            }
            return false;
        }
    }
}