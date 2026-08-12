using Auxom.Application.DTOs.DashBoard;
using Auxom.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IOrderService
    {

        Task PlaceOrderAsync(Guid userId,CreateOrderDto dto);
        Task<IEnumerable<OrderDto>> GetOrdersByUserAsync(Guid userId);
        Task<OrderDto> GetOrderByIdAsync(Guid userId ,  Guid orderId);


    }
}
