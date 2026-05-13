using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IOrdersService
    {
        Task<List<GiftOrdersDto>> GetOrdersByGiftAsync();
        Task<List<DtoGiftsUpdate>> GetGiftsOrderByPriceAsync();
        Task<List<DtoGifts_D>> GetGiftsOrderByOrdersAsync();
        Task<List<DtoUsers>> GetAllUsersAsync();
        Task<bool> CreateOrderAsync(int giftId, int userId);
        Task<bool> DeleteOrderAsync(int idOrder);
        Task<bool> UpdateOrderAsync(int idOrder);

    }
}
