using ProductService.Models.DTOs;
using ProductService.Models.Entities;

namespace ProductService.Extensions
{
    public static class MappingExtensions
    {
        public static ProductResponseDto ToProductResponseDto(this Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Status = product.Status.ToString(),
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public static Product ToProductEntity(this ProductCreateDto productCreateDto)
        {
            return new Product
            {
                Name = productCreateDto.Name,
                Description = productCreateDto.Description,
                Price = productCreateDto.Price,
                Category = productCreateDto.Category
            };
        }
    }
}