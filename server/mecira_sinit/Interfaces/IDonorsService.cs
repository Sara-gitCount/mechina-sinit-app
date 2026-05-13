using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IDonorsService
    {
        Task<List<DtoDonors>> GetAllDonors();
        Task<bool> CreateDonorAsync(DtoDonorsCreate donor);
        Task<bool> UpdateDonorAsync(DtoDonorsUpdate donor,int id);
        Task<bool> DeleteDonorAsync(int id);
        Task<DtoDonorsCreate> GetByNameAsync(string name);
        Task<DtoDonors> GetByEmailAsync(string email);
        Task<DtoDonors> GetByGiftAsync(string nameGift);
        Task<bool> AddDonotationAsync(int giftId, int donorId);
        Task<Donors> GetDonorByIdAsync(int id);
    }
}
