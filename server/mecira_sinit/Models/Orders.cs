using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Orders
    {
      public  int Id { get; set; }
        [Required]
        public int UserId { get; set; }
     public Useres User { get; set; }
        [Required]
        public int GiftId { get; set; }
        public Gifts Gift { get; set; }
        public bool Status { get; set; } = false;    
    }
}
