using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("gifts/[controller]")]
    public class GiftsController : Controller
    {
        private readonly IGiftsService giftsService;
        private readonly ILogger<GiftsController> logger;
        public GiftsController(IGiftsService giftsService, ILogger<GiftsController> logger) { 
            this.giftsService = giftsService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<DtoGifts>>> GetAllGiftsAsync()
        {
                var gifts = await giftsService.GetAllGiftsAsync();
                return Ok(gifts);
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        [Route("GetDonors/{id}")]
        public async Task<ActionResult<DtoDonors>> GetDonorsAsync(int id) //יש לו בעיה
        {
            try
            {
                var donors = await giftsService.GetDonorsAsync(id);
                return Ok(donors);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetGiftsByName/{name}")]
        public async Task<ActionResult<DtoGifts>> GetGiftsByNameAsync(string name)
        {
            try { 
            var gift=await giftsService.GetGiftsByNameAsync(name);
             if (gift == null)
                    return Ok(null);
                return Ok(gift);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetGiftsByNumOfUsersAsync")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<DtoGifts_D>>> GetGiftsByNumOfUsersAsync(int numOfUsers) //לא בדקתי
        {
            try { 
            var gifts=await giftsService.GetGiftsByNumOfUsersAsync(numOfUsers);
            return Ok(gifts);
            }
            //catch (KeyNotFoundException ex)
            //{
                //return NotFound(ex.Message);
            //}
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetGiftsByDonorName")]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<List<DtoGifts>>> GetGiftsByDonorNameAsync(string donorName)
        {
            try {
            var gifts= await giftsService.GetGiftsByDonorNameAsync(donorName);
            return Ok(gifts);
            }
           //catch (KeyNotFoundException ex)
           //{
           //    return NotFound(ex.Message);
           //}
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
       [Authorize(Roles = "manager")]
        public async Task<ActionResult> CreateGiftAsync(DtoGiftsUpdate gift)
        {
            try {
            await giftsService.CreateGiftAsync(gift);
            return Ok("The gift was created successfully!!!");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> DeleteGiftAsync(int id)
        {
            try {
            await giftsService.DeleteGiftAsync(id);
            return Ok("The gift was successfully deleted!!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> UpdateGiftAsync(DtoGiftsUpdate gift, int id)
        {
            try
            {
                await giftsService.UpdateGiftAsync(gift, id);
                return Ok("The gift has been updated successfully!!");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetOrderByPrice_CategoryAsync")]
       //[Authorize(Roles = "manager")] //בעיה במנהל בהרשאות
        public async Task<ActionResult<List<DtoGifts>>> GetOrderByPrice_CategoryAsync()
        {
                var gifts = await giftsService.GetOrderByPrice_CategoryAsync();
                return Ok(gifts);
         }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult<DtoGiftsUpdate>> GetByIdAsync(int id)
        {
            try
            {
                var gift = await giftsService.GetByIdAsync(id);
                return Ok(gift);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

