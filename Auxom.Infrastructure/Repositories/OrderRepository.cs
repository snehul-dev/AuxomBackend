using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using Auxom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AuxomContext _context;
        public OrderRepository(AuxomContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<Order?> GetByIdAsync(Guid userId, Guid orderId)
        {
           return  await _context.Orders.Include(o => o.OrderItems).
                ThenInclude(oi =>oi.Product)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId);

        }
        public async Task<int> GetTotalOrdersAsync()
        {
            return await _context.Orders.CountAsync();
        }
        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Orders.Where(o => o.Status == "Delivered").SumAsync(o => o.Total);
            
        }

        public async Task<Order?> GetOrderByIdWithItemsAsync(
         Guid userId,Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(
                    o => o.Id == orderId &&
                         o.UserId == userId);
        }

        public async Task<IEnumerable<Order>> GetOrderByUserAsync(Guid userId)
        {
            return await _context.Orders.
                Include(o =>o.OrderItems)
                .ThenInclude(oi =>oi.Product)
                . Where(o => o.UserId == userId).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.
                Include(o =>o.User)
                .Include(o =>o.OrderItems)
                .ToListAsync();
        }
        public async Task<Order?> GetOrderById(Guid OrderId)
        {
            return  await _context.Orders.FindAsync(OrderId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       

    }
}
