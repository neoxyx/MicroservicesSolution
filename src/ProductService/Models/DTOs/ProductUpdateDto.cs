using System.ComponentModel.DataAnnotations;
using ProductService.Models.Entities;

namespace ProductService.Models.DTOs
{
    public class ProductUpdateDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal? Price { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; }

        public ProductStatus? Status { get; set; }
    }
}