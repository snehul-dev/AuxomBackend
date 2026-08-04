using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using Auxom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AuxomContext _context;
        public CartItemRepository(AuxomContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CartItem cartitem)
        {
            await _context.CartItems.AddAsync(cartitem);
        }
        
        public void Delete(CartItem cartitem)
        {
            _context.CartItems.Remove(cartitem);
        }

        public async Task<CartItem?> GetByCartAndProductAsync(Guid cartid, Guid productid)
        {
            return await _context.CartItems.FirstOrDefaultAsync(c => c.CartId == cartid &&
                                c.ProductId == productid
            );
        }

        public async Task<CartItem?> GetByIdAsync(Guid id)
        {
            return await _context.CartItems.Include(ci => ci.Cart).FirstOrDefaultAsync(ci => ci.Id == id);
                           
        }

        public async Task<List<CartItem>> GetByCartIdAsync(Guid cartId)
        {
            return await _context.CartItems.Include(ci =>ci.Product).
                Where(c => c.CartId == cartId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(CartItem cartitem)
        {
            _context.CartItems.Update(cartitem);
        }
    }
}
