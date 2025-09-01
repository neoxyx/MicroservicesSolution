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
                Price = product.Price,
                Quantity = product.Quantity,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public static Product ToProductEntity(this ProductCreateDto productCreateDto)
        {
            return new Product
            {
                Name = productCreateDto.Name,
                Price = productCreateDto.Price,
                Quantity = productCreateDto.Quantity
            };
        }
    }
}