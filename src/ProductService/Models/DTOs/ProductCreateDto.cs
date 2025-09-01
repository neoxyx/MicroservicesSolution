// ProductCreateDto.cs
using System.ComponentModel.DataAnnotations;

namespace ProductService.Models.DTOs
{
    public class ProductCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}