using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using Auxom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AuxomContext _context;
        public WishlistRepository(AuxomContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Wishlist wishlist)
        {
            await _context.Wishlists.AddAsync(wishlist);
        }

        public async Task<Wishlist?> GetByUserAndProductAsync(Guid userId, Guid productId)
        {
            return await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
        }

        public async Task<IEnumerable<Wishlist>> GetWishlistByUserIdAsync(Guid userId)
        {
            return await _context.Wishlists.Include(w => w.Product).Where(w => w.UserId == userId).ToListAsync();
        }

        public void Remove(Wishlist wishlist)
        {
             _context.Wishlists.Remove(wishlist);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
