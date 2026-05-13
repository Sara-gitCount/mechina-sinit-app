using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto
{
    public class DtoUsers
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "המייל אינו תקין")]
        public string Email { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
    }
    public class UserResponseDto { 
    
        [Required]
        public int Id { get; set; } 
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Roles {  get; set; }
    }
}
