using AutoMapper;
using Auxom.Application.DTOs.Wishlist;
using Auxom.Application.Exceptions;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public WishlistService(IWishlistRepository wishlistRepository , IMapper mapper, IProductRepository productRepository)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task AddToWishlistAsync(Guid userId, CreateWishlistDto dto)
        {

            var product = await _productRepository.GetProductByIdAsync(dto.ProductId);
            if(product == null)
            {
                throw new NotFoundException("Product Not Found");
            }
            var existInWishlist = await _wishlistRepository.GetByUserAndProductAsync(userId, dto.ProductId);

            if(existInWishlist != null)
            {
                throw new BadRequestException("Product is already in the wishlist.");
            }

           var wishlist =  _mapper.Map<Wishlist>(dto);

            wishlist.UserId = userId;

            await _wishlistRepository.AddAsync(wishlist);
            await _wishlistRepository.SaveChangesAsync();


        }

        public async Task<IEnumerable<WishlistDto>> GetWishlistByUserIdAsync(Guid userId)
        {
            var wishlist = await _wishlistRepository.GetWishlistByUserIdAsync(userId);
    

           return _mapper.Map<IEnumerable<WishlistDto>>(wishlist);

           

        }

        public async Task RemoveWishlistAsync(Guid userId, Guid productId)
        {
            var existInWishlist = await _wishlistRepository.GetByUserAndProductAsync(userId, productId);
            if(existInWishlist == null)
            {
                throw new NotFoundException("Product not found in wishlist.");
            }
            _wishlistRepository.Remove(existInWishlist);

            await _wishlistRepository.SaveChangesAsync();

        }
    }
}
