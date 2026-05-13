using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Security;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class GiftsRepository : IGiftsRepository
    {
        private readonly StoreContext context;
        public GiftsRepository(StoreContext context) { 
         this.context = context;
        }

        public async Task<bool> CreateGiftAsync(Gifts gift)
        {
          context.gifts.Add(gift);
            await context.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteGiftAsync(int id)
        {
            var g =await context.gifts.FindAsync(id);
            if (g == null)
                return false;
            var orders=await context.orders.Where(orders => orders.GiftId== id).
                Select(orders => orders.Gift).ToListAsync();
            if (!orders.Any())
            {
                context.gifts.Remove(g);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Gifts>> GetAllGiftsAsync()//צריך להראות גם תקטוגריה
        {
            return await context.gifts.Include(g => g.Category)
                .ToListAsync(); 
        }

        public async Task<Gifts> GetByIdAsync(int id)
        {
            var gift =await context.gifts.FindAsync(id);
             return gift;
        }

        public async Task<Donors> GetDonorsAsync(int id)//change
        {
            var d = await context.gifts.
                Where(g => g.DonorId == id).
                Select(g=>g.Donor).
                FirstOrDefaultAsync();

            if (d == null)
                return null;
            return d;
        }

        public async Task<List<Gifts>> GetGiftsByDonorNameAsync(string donorName)
        {
            var gifts = await context.gifts.
                Where(e => e.Donor.FirstName + " " + e.Donor.LastName == donorName). 
                ToListAsync();
            return gifts;
        }

        public async Task<Gifts> GetGiftsByNameAsync(string name)
        {
            var gift = await context.gifts.
                Where(e => e.Name == name).FirstOrDefaultAsync();
            return gift;
        }

        public async Task<List<Gifts>> GetGiftsNumOfUsersByAsync(int numOfUsers)
        {
            var gift = await context.orders.
                Where(o => o.Status == true).
                GroupBy(o => o.GiftId).
                Where(g => g.Count() == numOfUsers).
                Select(g=>g.First().Gift).ToListAsync();
            return gift;
        }

        public async Task<bool> UpdateGiftAsync(Gifts gift)
        {
            var existing = await context.gifts.FindAsync(gift.Id);
            if (existing != null)
            {
                context.Entry(existing).CurrentValues.SetValues(gift);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Gifts>> GetOrderByPrice_CategoryAsync()
        {
            return await context.gifts.
                Include(g=>g.Category).
                OrderBy(g=>g.Price).
                ThenBy(g=>g.Category.Name).ToListAsync();   
        }
    }
}
