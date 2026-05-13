using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IDonorsRepository
    {
        Task<List<Donors>>GetAllDonorsAsync();
        Task<bool>CreateDonorAsync(Donors donor);
        Task<bool>DeleteDonorAsync(int id);
        Task<bool> UpdateDonorAsync(Donors donor);
        Task<Donors> GetDonorByIdAsync(int id); 
        Task <Donors> GetByNameAsync(string name);
        Task<Donors> GetByEmailAsync(string email);
        Task<Donors> GetByGiftAsync(string nameGift);
        Task<bool> AddDonotationAsync(int giftId, int donorId);
    }
}
