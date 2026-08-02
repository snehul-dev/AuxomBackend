using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task AddAsync(Cart cart);

        Task<List<Cart>> GetByUserIdAsync(Guid userId);

        Task<Cart?> GetByIdAsync(Guid cartId);

        Task<Cart?> GetByUserAndProductAsync(Guid userId, Guid productId);

        void Update(Cart cart);

        void Delete(Cart cart);

        Task SaveChangesAsync();
    }
}
