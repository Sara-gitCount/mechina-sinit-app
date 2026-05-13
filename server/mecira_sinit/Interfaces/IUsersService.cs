using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IUsersService
    {
        Task<List<DtoUsers>> GetAllUsersAsync();
        Task<DtoUsers> GetUserByIdAsync(int id);
        Task<DtoUsers> CreateUserAsync(Useres user);
        Task<DtoUsers> UpdateUserAsync(Useres user);
        Task<bool> DeleteUserAsync(int id);
        Task<LoginResponseDto?> AuthenticateAsync(string Email, string password);
        Task<bool> ExistingEmailAsync(string email);    
        Task<Useres>GetUserByEmailAsync(string email);
        Task<List<DtoBasket>> Basket(int idUser);
    }
}
