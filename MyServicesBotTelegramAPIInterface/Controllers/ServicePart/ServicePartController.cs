using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyServicesTelegramBotBussinessTier.Objects.ServicePart;
using MyServicesTelegramBotBussinessTier.Objects.ServicePart.Services;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDTO;

namespace MyServicesBotTelegramAPIInterface.Controllers.ServicePart
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePartController : ControllerBase
    {

        [HttpGet("GetAllServiceParts/{ServiceID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]

        public ActionResult<List<clsServicePartDTO>> GetAll(int ServiceID)
        {

            var Service = new clsGetAllServicePartsService();

            var Result = Service.Get(ServiceID);

            if(Service.exception != null)
            {
                return StatusCode(500,Service.exception.Message);
            }

            return Ok(Result);

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<int> AddServicePart(clsServicePartDTO servicePartDTO)
        {
            var ServicePart = new clsSaveServicePartService();

            var NewServicePart = new clsServicePart() { ServicePartDTO = servicePartDTO };

            var Result = ServicePart.Save(NewServicePart);

            if (Result)
            {
                return Ok(NewServicePart.ServicePartID);
            }

            return StatusCode(500, ServicePart.Exception.Message);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdateServicePart(clsUpdateServicePartDTO UpdateServicePartDTO)
        {
            if (UpdateServicePartDTO == null || UpdateServicePartDTO.ServicePartID == null)
                return BadRequest("The inputs Is Not Suitable");

            var ServicePart = new clsSaveServicePartService();

            Exception exception = null;

            var CurrentServicePart = clsServicePart.Find((int)UpdateServicePartDTO.ServicePartID, ref exception);

            if (CurrentServicePart == null)
                return NotFound();

            CurrentServicePart.UpdateServicePartDTO = UpdateServicePartDTO;

            var Result = ServicePart.Save(CurrentServicePart);

            if (Result)
            {
                return Ok();
            }

            return StatusCode(500, ServicePart.Exception.Message);
        }

    }
}
