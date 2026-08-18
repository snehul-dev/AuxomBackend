using Auxom.Application.DTOs.Product;
using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Auxom.Application.DTOs.Paginaton;

namespace Auxom.Application.Interfaces.Services
{
    public interface IProductService
    {


        Task<ProductDto?> GetProductByIdAsync(Guid id);

        Task<ProductDto> AddProductAsync(CreateProductDto dto);

        Task<PagedResultDto<ProductDto>> GetProductsAsync(
            int pageNumber,
            int pageSize);

        Task DeleteProductAsync(Guid id);

        Task UpdateProductAsync(Guid id, UpdateProductDto dto);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string keyword);
        Task<IEnumerable<ProductDto>> FilterProductsAsync(ProductFilter filter);

        

    
}
}
