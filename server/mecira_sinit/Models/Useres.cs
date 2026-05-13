using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Useres
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage ="המייל אינו תקין")]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        // [Phone]
        [RegularExpression(@"^(05\d{8}|02\d{7})$", ErrorMessage = "מספר טלפון חייב להתחיל ב־05 (10 ספרות) או ב־02 (9 ספרות)")]
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string Roles { get; set; } = "client";  
    }
}
