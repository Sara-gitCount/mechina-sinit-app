using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Dto;

namespace WebApplication1.Models
{
    public class Donors
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id{ get; set; }
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
        //public List<DtoGifts_D> Donations { get; set; }
        public List<Gifts> Donations { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
