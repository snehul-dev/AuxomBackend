using Auxom.Application.DTOs.Product;
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
        public async Task<int> GetTotalProductsAsync()
        {
           return await _context.Products.CountAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string keyword)
        {
            return await _context.Products.Where(p => p.Name.Contains(keyword)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterProductsAsync(ProductFilter filter)
        {
            IQueryable<Product> query = _context.Products;
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(p => p.Name.Contains(filter.Search));
            }

            if (!string.IsNullOrWhiteSpace(filter.Category))
            {
                query = query.Where(p => p.Category == filter.Category);
            }
            if (!string.IsNullOrWhiteSpace(filter.Color))
            {
                query = query.Where(p => p.Color == filter.Color);
            }
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }
            if (filter.InStock.HasValue)
            {
                query = query.Where(p => p.InStock == filter.InStock);
            }

            if (!string.IsNullOrWhiteSpace(filter.SortBy))
            {

                switch (filter.SortBy.ToLower())
                {
                    case "priceasc":

                        query = query.OrderBy(p => p.Price);
                        break;

                    case "pricedesc":

                        query = query.OrderByDescending(p => p.Price);
                        break;

                    case "rating":

                        query = query.OrderByDescending(p => p.Rating);
                        break;


                }
            }
        

            if (!string.IsNullOrWhiteSpace(filter.Category))
            {
                query = query.Where(p => p.Category == filter.Category);
            }

            return await query.ToListAsync();
        }

        public async Task<(List<Product> Products, int TotalCount)> GetPagedProductsAsync(
    int pageNumber,
    int pageSize)
        {
            var query = _context.Products
                .AsNoTracking()
                .OrderByDescending(p => p.CreatedAt);

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }


    }
}
