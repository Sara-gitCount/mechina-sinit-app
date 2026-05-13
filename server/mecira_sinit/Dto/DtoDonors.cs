using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Dto
{
    public class DtoDonors
    {
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
        public List<DtoGifts_D> Donations { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
    public class DtoDonorsCreate
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }

    public class DtoDonorsUpdate
    {
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
