using System.ComponentModel.DataAnnotations;
namespace SneakersAPI.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int UserId { get; set; }

    }
}
