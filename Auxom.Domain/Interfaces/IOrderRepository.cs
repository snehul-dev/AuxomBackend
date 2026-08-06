using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrderByUserAsync(Guid userId);
        Task<Order?> GetByIdAsync(Guid userId, Guid OrderId);
        Task SaveChangesAsync();
    }
}
