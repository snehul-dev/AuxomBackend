using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(Guid id);

        Task<IEnumerable<Product>> SearchProductsAsync(string keyword);
        Task<IEnumerable<Product>> FilterProductsAsync(ProductFilter filter);
        Task<int> GetTotalProductsAsync();
       
        Task<IEnumerable<Product>> GetProductsAsync();

        Task AddProductAsync(Product product);

        void DeleteProduct(Product product);

        void UpdateProduct(Product updatedProduct);

        Task SaveChangesAsync();

    }
}
