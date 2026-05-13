using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class UesrsService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly ILogger<UesrsService> logger;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        public UesrsService(IUsersRepository usersRepository, ILogger<UesrsService> logger,
                  ITokenService tokenService,
        IConfiguration configuration)
        {
            this.usersRepository = usersRepository;
            this.logger = logger;
            _tokenService = tokenService;
            _configuration = configuration;
        }
        public async Task<DtoUsers> CreateUserAsync(Useres user)
        {
            if (user == null)
            {
                logger.LogError("Failed to create a null user");
                throw new ArgumentNullException(nameof(user));
            }
            if (await ExistingEmailAsync(user.Email))
            {
                throw new ArgumentException("This email already exists");
            }
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("Password is required");
            }
            var newUser = new Useres
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = HashPassword(user.Password), // Simplified - use proper hashing
                Phone = user.Phone,
                Address = user.Address,
                Roles = user.Roles
            };
            var u = await usersRepository.CreateUserAsync(newUser);
            if (u == null)
            {
                logger.LogError("Failed to create user");
                throw new Exception("Errors adding user");
            }
            logger.LogInformation("User created");
            return MapRegilurToDto(u);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                logger.LogError("Failed to delete user with invalid id 0");
                throw new ArgumentException("Invalid id");
            }
            var user = await usersRepository.DeleteUserAsync(id);
            if (!user)
            {
                logger.LogError("User with id {Id} not found for deletion", id);
                throw new KeyNotFoundException($"User with id {id} was not found");
            }
            await usersRepository.DeleteUserAsync(id);
            logger.LogInformation("User with id {Id} deleted successfully", id);
            return user;
        }

        public async Task<List<DtoUsers>> GetAllUsersAsync()
        {
            var users = await usersRepository.GetAllUsersAsync();
            if (users == null)
                logger.LogWarning("No users found in GetAllUsers");
            return users.Select(MapRegilurToDto).ToList();
        }

        public async Task<DtoUsers> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                logger.LogError("Failed to get user with invalid id 0");
                throw new ArgumentException("Invalid id");
            }
            var user = await usersRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                logger.LogError("User with id {Id} not found", id);
                throw new KeyNotFoundException($"User with id {id} was not found");
            }
            return MapRegilurToDto(user);
        }

        public async Task<DtoUsers> UpdateUserAsync(Useres user)
        {
            if (user == null)
            {
                logger.LogError("Failed to update a null user");
                throw new ArgumentNullException(nameof(user));
            }
            var u = await usersRepository.UpdateUserAsync(user);
            if (u == null)
            {
                logger.LogError("Failed to update user");
                throw new Exception("Errors updating user");
            }
            logger.LogInformation("User updated");
            return MapRegilurToDto(u);
        }

        public async Task<LoginResponseDto?> AuthenticateAsync(string email, string password)
        {
            var user = await usersRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                logger.LogWarning("Login attempt failed: User not found for email {email}", email);
                return null;
            }

            // Verify password (simplified - in production use proper password verification)
            var hashedPassword = HashPassword(password);
            if (user.Password != hashedPassword)
            {
                logger.LogWarning("Login attempt failed: Invalid password for email {email}", email);
                return null;
            }

            var token = _tokenService.GenerateToken(user.Id, user.Email, user.FirstName, user.LastName, user.Roles);
            var expiryMinutes = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes", 60);

            logger.LogInformation("User {UserId} authenticated successfully", user.Id);

            return new LoginResponseDto
            {
                Token = token,
                TokenType = "Bearer",
                ExpiresIn = expiryMinutes * 60, // Convert to seconds
                User = MapToResponseDto(user)
            };
        }

        public async Task<bool> ExistingEmailAsync(string email)
        {
            var users = await usersRepository.GetAllUsersAsync();
            foreach (var user in users)
            {
                if (user.Email == email)
                    return true;
            }
            return false;
        }

        public async Task<List<DtoBasket>> Basket(int idUser)
        {
            if (idUser <= 0)
                throw new ArgumentException("Invalid user id");

            // קורא לריפוזיטורי שמחזיר List<Orders> עם Include של Gift
            var orders = await usersRepository.Basket(idUser);

            if (orders == null || !orders.Any())
                return new List<DtoBasket>();
            var result = orders
            .GroupBy(g => g. GiftId)
            .Select(g => new DtoBasket
             {
                 GiftId= g.Key,
                 Name = g.First().Gift.Name,
                 Description = g.First().Gift.Description,
                 Image = g.First().Gift.Image,
                Price = g.First().Gift.Price,
                Amount = g.Count(),
                OrderId = g.First().Id
            })
            .ToList();
            return result;  
        }

        private static DtoUsers MapRegilurToDto(Useres user)
        {
            return new DtoUsers
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone
            };
        }
        private static string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
        private static UserResponseDto MapToResponseDto(Useres user)
        {
            return new UserResponseDto
            {
                Id =user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
              Roles=user.Roles  //--
            };
        }

        public async Task<Useres> GetUserByEmailAsync(string email)
        {
            var user=await usersRepository.GetUserByEmailAsync(email);
            return user;
        }
    }
}
