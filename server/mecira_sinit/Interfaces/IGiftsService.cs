using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IGiftsService
    {
         Task<List<DtoGifts>> GetAllGiftsAsync();
         Task<bool> UpdateGiftAsync(DtoGiftsUpdate gift, int id);
         Task<bool> DeleteGiftAsync(int id);
         Task<bool> CreateGiftAsync(DtoGiftsUpdate gift);
         Task<DtoDonors> GetDonorsAsync(int id);
         Task<DtoGiftsUpdate> GetGiftsByNameAsync(string name);
         Task<List<DtoGifts>> GetGiftsByDonorNameAsync(string donorName);
         Task<List<DtoGifts_D>> GetGiftsByNumOfUsersAsync(int numOfUsers);
        Task<List<DtoGifts>> GetOrderByPrice_CategoryAsync();
        Task<DtoGiftsUpdate> GetByIdAsync(int id);
    }
}
