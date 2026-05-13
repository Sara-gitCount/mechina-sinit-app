using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly ILogger<OrdersService> logger;

        public OrdersService(IOrdersRepository ordersRepository, ILogger<OrdersService> logger)
        {
            this.ordersRepository = ordersRepository;
            this.logger = logger;
        }

        public async Task<List<DtoUsers>> GetAllUsersAsync()
        {
           var users=await ordersRepository.GetAllUsersAsync();
            if (users == null)
                logger.LogWarning("No users found in the database.");
            return users.Select(MapToResponseDto).ToList();
        }

        public async Task<List<GiftOrdersDto>> GetOrdersByGiftAsync()
        {
           var orders=await ordersRepository.GetOrdersByGiftAsync();
            if (orders == null)
                logger.LogWarning("No orders found in the database");
            return orders.Select(g => new GiftOrdersDto
            {
                GiftName = g.Key.Name,
                Users = g
              .Select(o => o.User.FirstName + " " + o.User.LastName)
              .ToList()
            }).ToList();
        }

        public async Task<List<DtoGifts_D>> GetGiftsOrderByOrdersAsync()
        {
            var gifts=await ordersRepository.GetGiftsOrderByOrdersAsync();
            if (gifts == null)
                logger.LogWarning("No gifts found in the database.");
            return gifts.Select(MapToResponseDto_D).ToList();
        }

        public async Task<List<DtoGiftsUpdate>> GetGiftsOrderByPriceAsync()
        {
            var gifts =await ordersRepository.GetGiftsOrderByPriceAsync();
            if (gifts == null)
                logger.LogWarning("No gifts found in the database.");
            gifts=gifts.GroupBy(g => g.Id).Select(g => g.First()).ToList();
            return gifts.Select (MapToResponseDto).ToList();
        }


        public async Task<bool> DeleteOrderAsync(int idOrder)
        {
            if (idOrder == 0)
            {
                logger.LogError("DeleteOrderAsync called with invalid idOrder: 0");
                throw new ArgumentException("Invalid idOrder");
            }
            var result =await ordersRepository.DeleteOrderAsync(idOrder);

            if (!result)
            {
                logger.LogError($"Failed to delete order with id {idOrder}");
                throw new KeyNotFoundException($"Failed to delete order with id {idOrder} the order has been completed. It is not possible to delete a completed order , or there is no such order in the database.");
            }
            logger.LogInformation($"Order with id {idOrder} deleted successfully.");    
            return result;
        }

        public async Task<bool> UpdateOrderAsync(int idOrder)
        {
            if (idOrder <= 0)
            {
                logger.LogError("UpdateOrderAsync called with invalid idOrder: 0");
                throw new ArgumentException("Invalid idOrder");
            }
            var result =await ordersRepository.UpdateOrderAsync(idOrder);
            if (!result)
            {
                logger.LogError($"Failed to update order with id {idOrder}");
                throw new KeyNotFoundException($"Failed to update order with id {idOrder}");
            }
            logger.LogInformation($"Order with id {idOrder} updated successfully.");    
            return result;
        }
        public async Task<bool> CreateOrderAsync(int giftId, int userId)
        {
            if (userId <= 0 || giftId <= 0)
            {
                logger.LogError("CreateOrderAsync called with invalid userId or giftId: 0");
                throw new ArgumentException("Invalid userId or giftId");
            }
            var result = await ordersRepository.CreateOrderAsync(giftId, userId);
            if (!result)
            {
                logger.LogError($"Failed to create order for userId {userId} and giftId {giftId}");
                throw new Exception("Failed to create order");
            }   
            logger.LogInformation($"Order created successfully for userId {userId} and giftId {giftId}.");  
            return result;
        }
        private static DtoGifts_D MapToResponseDto_D(Gifts gift)
        {
            return new DtoGifts_D
            {
                Name = gift.Name,
                Description = gift.Description,
                Image = gift.Image,
                CategoryId = gift.CategoryId,
            };
        }
        private static DtoGiftsUpdate MapToResponseDto(Gifts gift)
        {
            return new DtoGiftsUpdate
            {
                Name = gift.Name,
                Description = gift.Description,
                Image = gift.Image,
                CategoryId = gift.CategoryId,
                Price = gift.Price,
                DonorId = gift.DonorId,
            };
        }

        private static DtoUsers MapToResponseDto(Useres user)
        {
            return new DtoUsers
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
            };
        }
    }
}
