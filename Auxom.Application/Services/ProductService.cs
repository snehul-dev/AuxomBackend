using AutoMapper;
using Auxom.Application.DTOs.Paginaton;
using Auxom.Application.DTOs.Product;
using Auxom.Application.Exceptions;
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
            if (dto.Price < 0)
            {
                throw new BadRequestException("Price Cannot Be Negative");
            }
            var product = _mapper.Map<Product>(dto);

         
          
            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }
            

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if(product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            _productRepository.DeleteProduct(product);
            await _productRepository.SaveChangesAsync();


        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            return _mapper.Map<ProductDto>(product);
        }

  

        public async Task UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
           
            var product = await _productRepository.GetProductByIdAsync(id);

            if(product == null)
            {
                throw new NotFoundException("Product not found.");
            }
            
            if(dto.Price < 0)
            {
                throw new BadRequestException("Price Cannot Be Negative");
            }

            _mapper.Map(dto, product);
            product.UpdatedAt = DateTime.UtcNow;

            await _productRepository.SaveChangesAsync();
           

      
        }

       public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string keyword)
        {
           var products =  await _productRepository.SearchProductsAsync(keyword);
           return  _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<IEnumerable<ProductDto>> FilterProductsAsync(ProductFilter filter)
        {
            
            var products = await _productRepository.FilterProductsAsync(filter);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<PagedResultDto<ProductDto>> GetProductsAsync(
     int pageNumber,
     int pageSize)
        {
            if (pageNumber <= 0)
                pageNumber = 1;

            if (pageSize <= 0)
                pageSize = 10;

            if (pageSize > 100)
                pageSize = 100;

            var result = await _productRepository
                .GetPagedProductsAsync(pageNumber, pageSize);

            var products = _mapper.Map<List<ProductDto>>(result.Products);

            var totalPages = (int)Math.Ceiling(
                result.TotalCount / (double)pageSize);

            return new PagedResultDto<ProductDto>
            {
                Items = products,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = result.TotalCount,
                TotalPages = totalPages
            };
        }


    }
}
