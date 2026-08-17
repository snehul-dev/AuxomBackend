using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using Auxom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AuxomContext _context;
        public ReviewRepository(AuxomContext context)
        {
            _context = context;
        }
        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
          
        }

        public async Task<List<Review>> GetByProductIdAsync(Guid productId)
        {
            return await _context.Reviews.
                Include(r => r.User)
                .Where(r => r.ProductId == productId).ToListAsync();
            
        }

        public async Task<Review?> GetByUserIdAndProductIdAsync(Guid userId, Guid productId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == productId);
        }

       public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
