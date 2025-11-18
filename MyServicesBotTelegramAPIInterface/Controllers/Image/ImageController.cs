using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyServicesTelegramBotBussinessTier.Objects.Image.Services;
using MyServicesTelegramBotBussinessTier.Objects.Image;
using MyServicesTelegramBotDTO.ObjectsDTO.ImageDTO;

namespace MyServicesBotTelegramAPIInterface.Controllers.Image
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<int> AddImage(clsImageDTO imageDTO)
        {
            var Image = new clsSaveImageService();

            var NewImage = new clsImage() { ImageDTO = imageDTO };

            var Result = Image.Save(NewImage);

            if (Result)
            {
                return Ok(NewImage.ImageID);
            }

            return StatusCode(500, Image.Exception.Message);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdateImage(clsUpdateImageDTO UpdateImageDTO)
        {
            if (UpdateImageDTO == null || UpdateImageDTO.ImageID == null)
                return BadRequest("The inputs Is Not Suitable");

            var Image = new clsSaveImageService();

            Exception exception = null;

            var CurrentImage = clsImage.Find((int)UpdateImageDTO.ImageID, ref exception);

            if (CurrentImage == null)
                return NotFound();

            CurrentImage.UpdateImageDTO = UpdateImageDTO;

            var Result = Image.Save(CurrentImage);

            if (Result)
            {
                return Ok();
            }

            return StatusCode(500, Image.Exception.Message);
        }

    }
}
