using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MyServicesTelegramBotBussinessTier.Objects.Service;
using MyServicesTelegramBotBussinessTier.Objects.Service.Services;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicesDTO;

namespace MyServicesBotTelegramAPIInterface.Controllers.Service
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {

        [HttpGet("GetAll/{BotID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<List<clsUpdateServiceDTO>> GetAll([FromRoute] long BotID)
        {

            var Service = new clsGetAllServicesService();

            var Result = Service.Get(BotID);

            if(Service.Exception != null)
            {
                return StatusCode(500, Service.Exception.Message);
            }

            return Ok(Result);

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<int> AddService(clsServiceDTO serviceDTO)
        {

            var Service = new clsSaveServiceService();

            var NewService = new clsService() { ServiceDTO = serviceDTO };

            var Result = Service.Save(NewService);

            if (Result)
            {
                return Ok(NewService.ServiceID);
            }

            return StatusCode(500,Service.Exception.Message);

        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdateService(clsUpdateServiceDTO UpdateServiceDTO)
        {

            if (UpdateServiceDTO == null || UpdateServiceDTO.ServiceID == null) return BadRequest("The inputs Is Not Suitable");

            var Service = new clsSaveServiceService();

            Exception exception = null;

            var CurrentService = clsService.Find((int)UpdateServiceDTO.ServiceID,ref exception);

            if (CurrentService == null) return NotFound();

            CurrentService.UpdateServiceDTO = UpdateServiceDTO;

            var Result = Service.Save(CurrentService);

            if (Result)
            {
                return Ok();
            }

            return StatusCode(500, Service.Exception.Message);

        }


    }
}
