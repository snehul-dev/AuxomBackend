using Auxom.Application.DTOs.Product;
using Auxom.Domain.Entities;
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

        Task DeleteProductAsync(Guid id);

        Task UpdateProductAsync(Guid id, UpdateProductDto dto);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string keyword);
        Task<IEnumerable<ProductDto>> FilterProductsAsync(ProductFilter filter);

        

    
}
}
