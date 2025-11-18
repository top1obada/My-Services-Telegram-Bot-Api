using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyServicesTelegramBotBussinessTier.Objects.ServicePartDetails;
using MyServicesTelegramBotBussinessTier.Objects.ServicePartDetails.Services;
using MyServicesTelegramBotDataTier.Data.ServicePartDetails.Services;
using MyServicesTelegramBotDataTier.Data.ServicePartDetailsData;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDetailsDTO;
using MyServicesTelegramBotDTO.ObjectsDTO.ServicePartDTO;

namespace MyServicesBotTelegramAPIInterface.Controllers.ServicePart
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePartDetailsController : ControllerBase
    {
        [HttpGet("GetServicePartDetailsTitles/{ServicePartID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<List<clsServicePartDetailsTitleDTO>> GetServicePartDetailsTitles(int ServicePartID)
        {
            var service = new clsGetAllServicePartDetailsTitlesService();
            var result = service.Get(ServicePartID);

            if (service.exception != null)
            {
                return StatusCode(500, service.exception.Message);
            }

            return Ok(result);
        }

        [HttpGet("GetServicePartDetails/{ServicePartDetailsID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<clsServicePartDetailDTO> GetServicePartDetails(int ServicePartDetailsID)
        {
            var service = new clsGetServicePartDetailsService();
            var result = service.Get(ServicePartDetailsID);

            if (service.Exception != null)
            {
                return StatusCode(500, service.Exception.Message);
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<int> AddServicePartDetails(clsServicePartDetailDTO servicePartDetailsDTO)
        {
            var ServicePartDetails = new clsSaveServicePartDetailsService();

            var NewServicePartDetails = new clsServicePartDetail() { ServicePartDetailsDTO = servicePartDetailsDTO };

            var Result = ServicePartDetails.Save(NewServicePartDetails);

            if (Result)
            {
                return Ok(NewServicePartDetails.ServicePartDetailsID);
            }

            return StatusCode(500, ServicePartDetails.Exception.Message);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdateServicePartDetails(clsUpdateServicePartDetailDTO UpdateServicePartDetailsDTO)
        {
            if (UpdateServicePartDetailsDTO == null || UpdateServicePartDetailsDTO.ServicePartDetailsID == null)
                return BadRequest("The inputs Is Not Suitable");
            var ServicePartDetails = new clsSaveServicePartDetailsService();
            Exception exception = null;

            var CurrentServicePartDetails = clsServicePartDetail.Find((int)UpdateServicePartDetailsDTO.ServicePartDetailsID, ref exception);

            if (CurrentServicePartDetails == null)
                return NotFound();

            CurrentServicePartDetails.UpdateServicePartDetailsDTO = UpdateServicePartDetailsDTO;

            var Result = ServicePartDetails.Save(CurrentServicePartDetails);

            if (Result)
            {
                return Ok();
            }

            return StatusCode(500, ServicePartDetails.Exception.Message);
        }

    }
}
