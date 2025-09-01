using ProductService.Extensions;
using ProductService.Models.DTOs;
using ProductService.Models.Entities;
using ProductService.Repositories.Interfaces;
using ProductService.Services.Interfaces;

namespace ProductService.Services
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductServiceImpl> _logger;

        public ProductServiceImpl(
            IProductRepository repository,
            ILogger<ProductServiceImpl> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllProductsAsync();
            return products.Select(p => p.ToProductResponseDto());
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            return product.ToProductResponseDto();
        }

        public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto productDto)
        {
            var product = productDto.ToProductEntity();
            product.CreatedAt = DateTime.UtcNow;

            await _repository.AddProductAsync(product);
            return product.ToProductResponseDto();
        }

        public async Task<ProductResponseDto?> UpdateProductAsync(int id, ProductUpdateDto productDto)
        {
            var existingProduct = await _repository.GetProductByIdAsync(id);
            if (existingProduct == null) return null;

            // Aplicar los cambios del DTO al modelo
            if (productDto.Name is not null)
                existingProduct.Name = productDto.Name;

            if (productDto.Price.HasValue)
                existingProduct.Price = productDto.Price.Value;

            if (productDto.Quantity is not null)
                existingProduct.Quantity = productDto.Quantity.Value;

            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateProductAsync(existingProduct);
            return existingProduct.ToProductResponseDto();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteProductAsync(id);
        }
    }
}