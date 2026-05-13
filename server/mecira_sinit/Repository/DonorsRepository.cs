  using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class DonorsRepository : IDonorsRepository
    {
        private readonly StoreContext  context;

        public DonorsRepository(StoreContext context)
        {
            this.context = context;
        }
        public async Task<bool> CreateDonorAsync(Donors donor)
        {
            context .donors.Add(donor);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDonorAsync(int id)
        {
            var donor=await context.donors.FindAsync(id);
            if(donor == null)
                return false;
            context.donors.Remove(donor);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Donors>> GetAllDonorsAsync()
        {
            return await context.donors.
                Include(d => d.Donations).
                ToListAsync();
        }

        public async Task<bool> UpdateDonorAsync(Donors donor)
        {
            var d=await context.donors.FindAsync(donor.Id);
            if(d == null)
                return false;
            d.Phone = donor.Phone;
            d.Email = donor.Email;
            d.FirstName = donor.FirstName;
            d.LastName = donor.LastName;
            d.Donations = donor.Donations;
            d.Id = donor.Id;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Donors> GetDonorByIdAsync(int id)
        {
            return await context.donors.FindAsync(id);
        }

        public async Task<Donors> GetByNameAsync(string name)
        {
            var donor=await context.donors
                .Where(d => d.FirstName+" "+d.LastName == name).
                FirstOrDefaultAsync();
            return donor;
        }

        public async Task<Donors> GetByEmailAsync(string email)
        {
            var donor = await context.donors
           .Where(d => d.Email  == email).
           FirstOrDefaultAsync();
            return donor;
        }

        public async Task<Donors> GetByGiftAsync(string nameGift)
        {
            return await context.gifts.
                 Where(g => g.Name == nameGift).
                 Select(g=>g.Donor)
                 .FirstOrDefaultAsync();
        }

        public async Task<bool> AddDonotationAsync(int giftId, int donorId)
        {
            var donor = await context.donors.FindAsync(donorId);
            var gift= await context.gifts.FindAsync(giftId);    
            if (donor == null) 
                return false;
            donor.Donations.Add(gift);
            await context.SaveChangesAsync();   
            return true;
        }
    }
}
