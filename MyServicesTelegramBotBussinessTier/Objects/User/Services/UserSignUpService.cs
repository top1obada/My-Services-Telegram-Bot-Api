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
    public class clsUserSignUpService : ISignUpService<int?,clsUserLoginInfo>
    {
        public Exception Exception;
        public int ? SignUp(clsUserLoginInfo userLoginInfo)
        {

            var Result = clsPasswordEncrypt.HashPassword(userLoginInfo.Password);



            return clsUserData.UserSignUp(new clsUserDTO()
            {
                PasswordHash = Result.Hash,
                Salt = Result.Hash,
                UserName = userLoginInfo.UserName,
                JoiningDate = DateTime.Now
            }, ref Exception);
        }



    }
}
