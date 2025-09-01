using System.ComponentModel.DataAnnotations;
using ProductService.Models.Entities;

namespace ProductService.Models.DTOs
{
    public class ProductUpdateDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal? Price { get; set; }

        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; }
    }
}