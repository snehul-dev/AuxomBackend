using Auxom.Application.DTOs.Cart;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Interfaces;
using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Auxom.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository,
                           ICartItemRepository cartItemRepository,
                           IProductRepository productRepository,
                           IMapper mapper
                           )
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddCartAsync(Guid userid ,AddCartDto dto)
        {
            if(dto.Quantity <= 0)
            {
                throw new Exception("Quantity must be greater than zero.");
            }
            var product = await _productRepository.GetProductByIdAsync(dto.ProductId);
            if(product == null)
            {
                throw new Exception("Product not found");
            }

            var usercart = await _cartRepository.GetCartByUserIdAsync(userid);
            if(usercart == null)
            {
                usercart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userid
                };
                await _cartRepository.AddCartAsync(usercart);
                await _cartRepository.SaveChangesAsync();
            }

           

            var cartItem = await _cartItemRepository.GetByCartAndProductAsync(usercart.Id, dto.ProductId);
            if(cartItem != null)
            {
                var newQuantity = cartItem.Quantity + dto.Quantity;
                if (newQuantity > product.StockQuantity)
                {
                    throw new Exception("Requested quantity exceeds available stock.");
                }

                cartItem.Quantity = newQuantity;

                _cartItemRepository.Update(cartItem);
            
            }
            else
            {
                if(dto.Quantity > product.StockQuantity)
                {
                    throw new Exception("Requested quantity exceeds available stock.");
                }
                cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = usercart.Id,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                await _cartItemRepository.AddAsync(cartItem);
            }

            await _cartItemRepository.SaveChangesAsync();

        }

        public async Task ClearCartAsync(Guid userId)
        {
            var usercart = await _cartRepository.GetCartByUserIdAsync(userId);
             if (usercart == null)
            {
                throw new Exception("Cart not found");
            }

            var cartItems = await _cartItemRepository.GetByCartIdAsync(usercart.Id);
            foreach(var item in cartItems)
            {
                _cartItemRepository.Delete(item);
            }

            usercart.UpdatedAt = DateTime.UtcNow;
            _cartRepository.UpdateCart(usercart);

           
            await _cartRepository.SaveChangesAsync();
            
        }

        public async Task<CartDto> GetCartAsync(Guid userid)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userid);
            if(cart == null)
            {
                throw new Exception("Cart not found");
            }

            return _mapper.Map<CartDto>(cart);


        }

        public async Task<bool> RemoveCartItemAsync(Guid cartitemid)
        {
            var cartitem = await _cartItemRepository.GetByIdAsync(cartitemid);

            if(cartitem == null)
            {
                return false;
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(cartitem.Cart.UserId);
            if(cart != null)
            {
                cart.UpdatedAt = DateTime.UtcNow;
                _cartRepository.UpdateCart(cart);
            }

            _cartItemRepository.Delete(cartitem);

       
            await _cartRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateQuantityAsync(Guid cartitemid, int quantity)
        {
            if(quantity <= 0)
            {
                return false;
            }

            var cartitem = await _cartItemRepository.GetByIdAsync(cartitemid);
            if(cartitem == null)
            {
                return false;
            }

            var product = await _productRepository.GetProductByIdAsync(cartitem.ProductId);
            if(product  == null)
            {
                return false;
            }

            if(quantity > product.StockQuantity)
            {
                throw new Exception("Requested quantity exceeds available stock.");
            }

            cartitem.Quantity = quantity;
            _cartItemRepository.Update(cartitem);

            var cart = await _cartRepository.GetCartByUserIdAsync(cartitem.Cart.UserId);
            if(cart != null)
            {
                cart.UpdatedAt = DateTime.UtcNow;
                _cartRepository.UpdateCart(cart);
            }

            await _cartRepository.SaveChangesAsync();
          

            return true;

           
        }
    }
}
