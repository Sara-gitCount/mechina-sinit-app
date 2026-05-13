using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<Useres>> GetAllUsersAsync();
        Task<Useres> GetUserByIdAsync(int id);
        Task<Useres> CreateUserAsync(Useres user);
        Task<Useres> UpdateUserAsync(Useres user);
        Task<bool> DeleteUserAsync(int id);
        Task<Useres> GetUserByEmailAsync(string email);
        Task<List<Orders>> Basket(int idUser);
    }
}
