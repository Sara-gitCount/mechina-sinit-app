using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers;
    [ApiController]
   [Route("users/[controller]")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> logger;
        private readonly IUsersService usersService;

        public UsersController(ILogger<UsersController> logger, IUsersService usersService)
        {
            this.logger = logger;
            this.usersService = usersService;
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<DtoUsers>>> GetAllUsers()
        {
            var users = await usersService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]//------לבדוק בכל השאר 
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<DtoUsers>> GetUserById(int id)
        {
            try
            {
                var user = await usersService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("basket/{idUser}")]
        public async Task<List<DtoBasket>> Basket(int idUser)
        {
            try
            {
                var orders = await usersService.Basket(idUser);
                return orders;
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, "Error retrieving basket for user {UserId}", idUser);
                throw;
            }
        }
    }

