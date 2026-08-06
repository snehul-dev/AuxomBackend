using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(Guid userid);
        Task AddCartAsync(Cart cart);
        void UpdateCart(Cart cart);
        void ClearCart( Cart cart);
        Task SaveChangesAsync();
    }
}
