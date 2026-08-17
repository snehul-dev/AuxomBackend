using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Auxom.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task AddReviewAsync(Review review);
        Task <List<Review>> GetByProductIdAsync(Guid productId);
        Task<Review?> GetByUserIdAndProductIdAsync(Guid userId, Guid productId);
        Task SaveChangesAsync();

    }
}
