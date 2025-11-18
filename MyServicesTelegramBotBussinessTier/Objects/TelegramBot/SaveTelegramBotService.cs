using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.TelegramBotData;
using MyServicesTelegramBotDTO.ObjectsDTO.TelegramBotDTO;

namespace MyServicesTelegramBotBussinessTier.Objects.TelegramBot
{
    public class clsSaveTelegramBotService : ISaveService<bool,clsTelegramBotDTO>
    {
        public Exception Exception;
        public bool Save(clsTelegramBotDTO TelegramBotDTO)
        {

            var Result = clsTelegramBotData.InsertTelegramBot(TelegramBotDTO, ref Exception);

            return Result;


        }

    }
}
