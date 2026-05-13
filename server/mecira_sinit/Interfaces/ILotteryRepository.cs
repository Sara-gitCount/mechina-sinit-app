using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ILotteryRepository
    {
        Task<List<Useres>> LotteryAsync(Gifts gift);
         Task<List<Lottery>> GetAllWinnersAsync();
         Task<int> GetAllRevenueAsync();
        //Task<bool> SendingEmailAsync(Useres user, string nameGift);
        Task<bool> AddLotteryAsync(int idGift, int idUser);
        Task<bool> LotteryDone();
    }
}
