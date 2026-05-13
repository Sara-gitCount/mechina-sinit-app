using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IOrdersRepository
    {
        Task<List<IGrouping<Gifts, Orders>>> GetOrdersByGiftAsync();
        Task<List<Gifts>> GetGiftsOrderByPriceAsync();
        Task<List<Gifts>> GetGiftsOrderByOrdersAsync();
        Task<List<Useres>> GetAllUsersAsync();
        Task<bool> CreateOrderAsync(int giftId, int userId);
        Task<bool> DeleteOrderAsync(int idOrder); 
        Task<bool> UpdateOrderAsync(int idOrder);
    }
}
