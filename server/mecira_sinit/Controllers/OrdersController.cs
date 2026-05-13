using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Orders/ [Controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly ILogger<OrdersController> logger;
        public OrdersController(IOrdersService ordersService, ILogger<OrdersController> logger)
        {
            this.ordersService = ordersService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<DtoUsers>>> GetAllUsersAsync()
        {
            return await ordersService.GetAllUsersAsync();
        }

        [HttpGet]
        [Route("GetGiftsOrderByOrders")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<DtoGifts_D>>> GetGiftsOrderByOrdersAsync()
        {
            return await ordersService.GetGiftsOrderByOrdersAsync();
        }

        [HttpGet]
        [Route("GetGiftsOrderByPrice")]
         [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<DtoGiftsUpdate>>> GetGiftsOrderByPriceAsync()
        {
            return await ordersService.GetGiftsOrderByPriceAsync();
        }

        [HttpDelete("{idOrder}")]
         [Authorize]
        public async Task<ActionResult> DeleteOrderAsync(int idOrder)
        {
            try
            {
                var d = await ordersService.DeleteOrderAsync(idOrder);
                return Ok("Product deleted successfully!!");
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

        [HttpPost("{giftId}/{userId}")]
        [Authorize]
        public async Task<ActionResult> CreateOrderAsync(int giftId, int userId)
        {
            try
            {
                var c = await ordersService.CreateOrderAsync(giftId, userId);
                return Ok("Product added successfully!!!");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{idOrder}")]
        [Authorize]
        public async Task<ActionResult> UpdateOrderAsync(int idOrder)
        {
            try
            {
               var u= await ordersService.UpdateOrderAsync(idOrder);
                return Ok("Thank you for shopping with us!!");
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
        [Route("GetOrdersByGift")]
        [Authorize(Roles = "manager")]
        public async Task<List<GiftOrdersDto>> GetOrdersByGiftAsync()
        {
                var orders= await ordersService.GetOrdersByGiftAsync();
            return orders;
        }
    }
}
    
