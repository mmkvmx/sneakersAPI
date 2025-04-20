using System.ComponentModel.DataAnnotations;
namespace SneakersAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId{ get; set; }
        public string ItemsJson { get; set; }
        public int TotalPrice {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
