using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class LotteryRepository : ILotteryRepository
    {
        private readonly StoreContext context;
        public LotteryRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<int> GetAllRevenueAsync()
        {
            return await context.orders
                  .Select(g => g.Gift.Price).SumAsync();
        }

        public Task<List<Lottery>> GetAllWinnersAsync()
        {
            //return context.lotteries.ToListAsync();
            var l=context.lotteries
                .Include(l => l.Gift)
                .Include(l => l.User)
                .ToListAsync();
            return l;
        }

        public async Task<List<Useres>> LotteryAsync(Gifts gift)
        {
            return await context.orders.
                Where(o => o.GiftId == gift.Id).
                Select(o => o.User).ToListAsync();
        }



        public async Task<bool> AddLotteryAsync(int idUser, int idGift)
        {
            var gift = await context.gifts.FindAsync(idGift);
            var user = await context.useres.FindAsync(idUser);

            if (gift == null || user == null)
                return false;

            var lottery = new Lottery
            {
                GiftId = gift.Id,
                UserId = user.Id
            };

            context.lotteries.Add(lottery);
            await context.SaveChangesAsync();  
            return true;
        }

        public async Task<bool> LotteryDone()
        {
            return context.lotteries.Any();
        }
    }
}
