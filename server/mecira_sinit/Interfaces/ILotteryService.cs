using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ILotteryService
    {
        Task<bool> LotteryAsync();
        Task<List<DtoLottery>> GetAllWinnersAsync();
        Task<int> GetAllRevenueAsync();
        Task SendingEmailAsync(Useres user, string nameGift);
        Task<bool> LotteryDone();
    }
}
