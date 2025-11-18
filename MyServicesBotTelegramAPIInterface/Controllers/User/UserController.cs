using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyServicesTelegramBotBussinessTier.Objects.User.Services;
using MyServicesTelegramBotDTO.ObjectsDTO.UserDTO;
using ProjectsServices.JWTServices;
namespace MyServicesBotTelegramAPIInterface.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost("SignUp")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<string> SignUp(clsUserLoginInfo UserInfo)
        {

            var Service = new clsUserSignUpService();

            var Result = Service.SignUp(UserInfo);

            if(Result == null)
            {
                return StatusCode(500, Service.Exception.Message);
            }

            var Token = clsJWTHelper.GenerateJwtToken
                (new Claim[]
            {new Claim ("UserID",Result.ToString()),
            }, clsJWTHelper.GetToken(_configuration));

            return Ok(Token);


        }


        [HttpGet("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<string> Login(clsUserLoginInfo UserLoginInfo)
        {
            var Service = new clsUserLoginService();

            var Result = Service.Login(UserLoginInfo);

            if(Result == null)
            {
                return StatusCode(500, Service.Exception.Message);
            }

            var Token = clsJWTHelper.GenerateJwtToken(new Claim[]
            {new Claim("UserID",Result.UserID.ToString())}, clsJWTHelper.GetToken(_configuration));

            return Ok(Token);
        }


    }
}
