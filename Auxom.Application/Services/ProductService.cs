using AutoMapper;
using Auxom.Application.DTOs.Product;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> AddProductAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            if(product.Price < 0)
            {
                throw new Exception("Price Cannot Be Negative");
            }
          
            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }
            

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if(product == null)
            {
                return false;
            }

            _productRepository.DeleteProduct(product);
            await _productRepository.SaveChangesAsync();

            return true;

        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<bool> UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
           
            var product = await _productRepository.GetProductByIdAsync(id);

            if(product == null)
            {
                return false;
            }
            
            if(product.Price < 0)
            {
                throw new Exception("Price Cannot Be Negative");
            }

            _mapper.Map(dto, product);
            product.UpdatedAt = DateTime.UtcNow;

            await _productRepository.SaveChangesAsync();
            return true;

      
        }
    }
}
