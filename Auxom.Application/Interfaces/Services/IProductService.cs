using Auxom.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDto>> GetProductsAsync();

        Task<ProductDto?> GetProductByIdAsync(Guid id);

        Task<ProductDto> AddProductAsync(CreateProductDto dto);

        Task<bool> DeleteProductAsync(Guid id);

        Task<bool> UpdateProductAsync(Guid id, UpdateProductDto dto);
    
}
}
