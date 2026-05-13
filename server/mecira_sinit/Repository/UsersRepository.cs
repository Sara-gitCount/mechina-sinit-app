using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly StoreContext context;
        public UsersRepository(StoreContext context)
        { 
            this.context = context;
        }

        public async Task<List<Orders>> Basket(int idUser)
        {
            var orders = await context.orders.
           Where(o=>o.Status==false)
          .Include(o => o.Gift)
          .Where(o => o.UserId == idUser)
          .ToListAsync();
            return orders;
        }

        public async Task<Useres> CreateUserAsync(Useres user)
        {
            context.useres.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await context.useres.FindAsync(id);
            if(user == null)
                return false;
            context.useres.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Useres>> GetAllUsersAsync()
        {
            return await context.useres.ToListAsync();
        }

        public async Task<Useres> GetUserByEmailAsync(string email)
        {
            var user=await context.useres.FirstOrDefaultAsync(u=>u.Email==email);
            return user;
        }

        public async Task<Useres> GetUserByIdAsync(int id)
        {
            var user =await context.useres.FindAsync(id);    
            return user;
        }

        public async Task<Useres> UpdateUserAsync(Useres user)
        {
            var u= await context.useres.FindAsync(user.Id);
            if(u == null)
                return null;    
            u.FirstName = user.FirstName;
            u.Password = user.Password;
            u.LastName = user.LastName;
            u.Email = user.Email;
            u.Phone = user.Phone;
            u.Address = user.Address;
            await context.SaveChangesAsync();
            return u;
        }
    }
}
