using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("lottery/[controller]")]
    public class LotteryController : Controller
    {
        private readonly ILotteryService lotteryService;
        private readonly ILogger<LotteryController> logger;
        public LotteryController(ILotteryService lotteryService, ILogger<LotteryController> logger)
        {
            this.lotteryService = lotteryService;
            this.logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> LotteryAsync()
        {
            try {
            await lotteryService.LotteryAsync();
            return Ok("ההגרלה נעשתה בהצלחה!");
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the lottery.");
            }
        }

        [HttpGet]
        [Route("GetAllWinners")]
        public async Task<ActionResult<List<DtoLottery>>> GetAllWinnersAsync()
        {
            try
            {
                var winners = await lotteryService.GetAllWinnersAsync();
                return winners;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllRevenue")]
        [Authorize(Roles = "manager")]
        public  async Task<ActionResult<int>> GetAllRevenueAsync()
        {
            try
            {
                var price = await lotteryService.GetAllRevenueAsync();
                return price;
            }
                        catch(KeyNotFoundException ex)
            {
               return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("sendingEmail/{nameGift}")]
        public async Task<ActionResult> SendingEmailAsync([FromBody] Useres user, string nameGift)
        {
            try
            {
                await lotteryService.SendingEmailAsync(user, nameGift);
                return Ok("Email sent");
            }
            catch (KeyNotFoundException ex)
            {
               NotFound(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("LotteryDone")]
        public async Task<ActionResult> LotteryDone()
        {
            return Ok(await lotteryService.LotteryDone());
        }
    }
}
