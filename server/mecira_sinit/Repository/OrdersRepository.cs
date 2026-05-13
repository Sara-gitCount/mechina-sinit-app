using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly  StoreContext context;
        public OrdersRepository(StoreContext context) {
            this.context = context;
        }

        public async Task<bool> CreateOrderAsync(int giftId, int userId)
        {
           var order=new Orders
            {
                GiftId=giftId,
                UserId=userId
            };
            context.orders.Add(order);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int idOrder)
        {
            var order=await context.orders.FindAsync(idOrder);
            if (order == null)
                return false;
            if (order.Status == false)
            {
                context.orders.Remove(order);
                 await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Useres>> GetAllUsersAsync()
        {
            var users=await context.orders.
            Where(u => u.Status == true).
            Select(u=>u.User).
            Distinct().ToListAsync();
            return users;
        }

        public async Task<List<Gifts>> GetGiftsOrderByOrdersAsync()
        {
            var gifts = await context.orders.
                Where(u => u.Status == true).
                GroupBy(e => e.Gift).
                OrderByDescending(e => e.Count()).
                Select(e => e.Key).ToListAsync();
            return gifts;
        }

        public async Task<List<Gifts>> GetGiftsOrderByPriceAsync()
        {
            var gifts = await context.orders.
                  Where(u => u.Status == true).
                Select(e => e.Gift).
                OrderByDescending(e => e.Price).ToListAsync();
            return gifts;

        }

        public async Task<List<IGrouping<Gifts, Orders>>> GetOrdersByGiftAsync()
        {
            var orders = await context.orders.
                Where(o=>o.Status == true).
                Include(o => o.Gift).
                Include(o => o.User).
                GroupBy(o => o.Gift).ToListAsync();
            return  orders;
        }

        public async Task<bool> UpdateOrderAsync(int idOrder)
        {
            var order = await context.orders.FindAsync(idOrder);
            if (order == null)
                return false;
            var orders = await context.orders.
                Where(o => o.GiftId==order.GiftId &&
                o.UserId==order.UserId && o.Status==false)
                .ExecuteUpdateAsync(setters =>
        setters.SetProperty(o => o.Status, true));
            return true;
        }
    }
}
