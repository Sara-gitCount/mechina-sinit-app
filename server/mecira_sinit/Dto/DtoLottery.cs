using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Dto
{
    public class DtoLottery
    {
        public Useres User { get; set; }
        [Required]
        public string GiftName { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
