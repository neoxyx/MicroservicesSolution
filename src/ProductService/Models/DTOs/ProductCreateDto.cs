// ProductCreateDto.cs
using System.ComponentModel.DataAnnotations;

namespace ProductService.Models.DTOs
{
    public class ProductCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, MaxLength(50)]
        public string Category { get; set; }
    }
}