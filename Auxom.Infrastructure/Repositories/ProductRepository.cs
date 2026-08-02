using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using Auxom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly AuxomContext _context;
        public ProductRepository(AuxomContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void DeleteProduct(Product product)
        {
                _context.Products.Remove(product); 
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateProduct(Product updatedProduct)
        {
            _context.Products.Update(updatedProduct);
        }
    }
}
