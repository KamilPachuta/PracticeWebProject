using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class CreateRestaurantDto
    {

        public int Id { get; set; }
        
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Category { get; set; }
        
        [Required]
        public bool HasDelivery { get; set; }
        
        [Required]
        public string ContactEmail { get; set; }
        public string? ContactNumber { get; set; }

        
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        
        [Required]
        public string PostalCode { get; set; }
    }
}
