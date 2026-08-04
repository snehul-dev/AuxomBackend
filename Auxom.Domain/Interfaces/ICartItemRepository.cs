using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetByIdAsync(Guid id);
        Task<CartItem?> GetByCartAndProductAsync(Guid cartid, Guid productid);
        Task AddAsync(CartItem cartitem);
        void Update(CartItem cartitem);
        void Delete(CartItem cartitem);
        Task<List<CartItem>> GetByCartIdAsync(Guid cartId);
        Task SaveChangesAsync();

    }
}
