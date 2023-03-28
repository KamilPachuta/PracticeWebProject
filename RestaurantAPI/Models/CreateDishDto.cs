using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class CreateDishDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        
        [Required]
        public Decimal Price { get; set; }

        public int RestaurantID { get; set; }
    }
}
