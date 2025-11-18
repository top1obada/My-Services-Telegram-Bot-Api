using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyServicesTelegramBotBussinessTier.Objects.TelegramBot;
using MyServicesTelegramBotDataTier.Data.TelegramBotData;
using MyServicesTelegramBotDTO.ObjectsDTO.TelegramBotDTO;
using Telegram.Bot;

namespace MyServicesBotTelegramAPIInterface.Controllers.TelegramBot
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramBotController : ControllerBase
    {
        
        [HttpPost("InitBot")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> InitBot([FromHeader] string Token)
        {

            var Bot = new TelegramBotClient(Token);

            var Info = await Bot.GetMeAsync();

            if (Info == null) return StatusCode(500);

            var TelegramBotDTO = new clsTelegramBotDTO()
            {
                BotID = Info.Id,
                BotUserName = Info.Username,
                BotName = Info.FirstName,
                CreatedDate = DateTime.Now,
                UserID
             = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value)
            };

            var Service = new clsSaveTelegramBotService();

            var Result = Service.Save(TelegramBotDTO);

            if (Result)
            {
                await Bot.SetWebhookAsync($"https://myservicestelegrambot.runasp.net/api/Bot/{Token}/{Info.Id}");

                return Ok();
            }

            else
            {
                return StatusCode(500, Service.Exception.Message);
            }

        }
    }
}
