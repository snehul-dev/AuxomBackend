using AutoMapper;
using Auxom.Application.DTOs.DashBoard;
using Auxom.Application.DTOs.Order;
using Auxom.Application.Exceptions;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly ICartRepository _cartRepository;
     
        public OrderService(IOrderRepository orderRepository, IMapper mapper,
             IAddressRepository addressRepository, ICartRepository cartRepository
            
            )
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _cartRepository = cartRepository;
           
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid userId, Guid orderId)
        {


           var order = await _orderRepository.GetByIdAsync(userId, orderId);
            if(order == null)
            {
                throw new NotFoundException("Order not found");
            }
          return   _mapper.Map<OrderDto>(order);

        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(Guid userId)
        {
            var orders = await _orderRepository.GetOrderByUserAsync(userId);
            if (!orders.Any())
            {
                throw new NotFoundException("Orders not found");
            }
           return  _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task PlaceOrderAsync(Guid userId, CreateOrderDto dto)
        {
            var address = await _addressRepository.GetByIdAsync(dto.AddressId);
            if(address == null)
            {
                throw new NotFoundException("For Delivery an address Required");
            }
            if(address.UserId != userId)
            {
                throw new UnauthorizedException("This address does not belong to you."); 
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if(cart == null || !cart.CartItems.Any())
            {
                throw new BadRequestException("Cart is empty");
            }

            foreach(var item in cart.CartItems)
            {
                var product = item.Product;

                if (!product.InStock)
                {
                    throw new BadRequestException($"{product.Name} is out of stock");
                }

                if(product.StockQuantity < item.Quantity)
                {
                    throw new NotFoundException($"Only {product.StockQuantity} quantity available for {product.Name}.");
                }
                

        
            }
            var grandTotal = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity);
            var order = new Order
            {
                UserId = userId,
                AddressId = dto.AddressId,
                Total = grandTotal,
                PaymentMethod = dto.PaymentMethod
            };

            foreach(var item in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }

            await _orderRepository.AddOrderAsync(order);
           

            foreach (var item in cart.CartItems)
            {
                item.Product.StockQuantity -= item.Quantity;
                if(item.Product.StockQuantity == 0)
                {
                    item.Product.InStock = false;
                }
            }

            _cartRepository.ClearCart(cart);
            await _orderRepository.SaveChangesAsync();
        }

  
    }
}
