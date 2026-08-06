using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using Auxom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AuxomContext _context;
        public CartRepository(AuxomContext context)
        {
            _context = context;
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public async Task<Cart?> GetCartByUserIdAsync(Guid userid)
        {
            return  await _context.Carts.
                Include(c =>c.CartItems)
                .ThenInclude(ci =>ci.Product)
                .FirstOrDefaultAsync(c =>c.UserId == userid);

        }
        public void ClearCart(Cart cart)
        {
            _context.Carts.Remove(cart);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
        }
    }
}
