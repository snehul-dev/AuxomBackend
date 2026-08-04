using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Interfaces
{
    public interface IWishlistRepository
    {
        Task AddAsync(Wishlist wishlist);
        Task <IEnumerable <Wishlist>> GetWishlistByUserIdAsync(Guid userId);
        Task<Wishlist> GetByUserAndProductAsync(Guid userId, Guid ProductId);
        void Remove(Wishlist wishlist);
        Task SaveChangesAsync();
    }
}
