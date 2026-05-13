using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto
{
        public class LoginRequestDto
        {
        [Required]
        [EmailAddress(ErrorMessage = "המייל אינו תקין")]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "הסיסמה חייבת להיות לפחות 6 תווים")]
        public string Password { get; set; } = string.Empty;
        }

        public class LoginResponseDto
        {
            public string Token { get; set; } = string.Empty;
            public string TokenType { get; set; } = "Bearer";
            public int ExpiresIn { get; set; }
            public UserResponseDto User { get; set; } = null!;
        }
    }
