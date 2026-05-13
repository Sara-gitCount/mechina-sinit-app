using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IGiftsRepository
    {
         Task<List<Gifts>> GetAllGiftsAsync();
         Task<bool> UpdateGiftAsync(Gifts gift);
         Task<bool> DeleteGiftAsync(int id);
         Task<bool> CreateGiftAsync(Gifts gift);
         Task<Donors> GetDonorsAsync(int id);
         Task<Gifts> GetGiftsByNameAsync(string name);
         Task<List<Gifts>> GetGiftsByDonorNameAsync(string donorName);
         Task<List<Gifts>> GetGiftsNumOfUsersByAsync(int numOfUsers);
         Task<Gifts> GetByIdAsync(int id); 
        Task<List <Gifts>> GetOrderByPrice_CategoryAsync();
    }
}
