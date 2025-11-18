using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServicesTelegramBotBussinessTier.Services;
using MyServicesTelegramBotDataTier.Data.UserData;
using MyServicesTelegramBotDTO.ObjectsDTO.UserDTO;
using ProjectsServices.PasswordServices;

namespace MyServicesTelegramBotBussinessTier.Objects.User.Services
{
    public class clsUserLoginService : ILoginService<clsUserDTO,clsUserLoginInfo>
    {
        public Exception Exception;
        public clsUserDTO Login(clsUserLoginInfo loginInfo)
        {

            var Result = clsUserData.UserLogin(loginInfo.UserName, ref Exception);

            if (Result == null) return null;

            var IsPasswordCorrect = clsPasswordEncrypt.VerifyPassword(loginInfo.Password, Result.PasswordHash, Result.Salt);

            if (IsPasswordCorrect)
            {
                Result.UserName = loginInfo.UserName;
                return Result;
            }

            return null;

        }

    }
}
