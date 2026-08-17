using Auxom.Application.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task<List<ReviewResponseDto>> GetByProductAsync(Guid productId);
        Task AddReviewAsync(Guid userId, Guid productId, CreateReviewDto dto);
    }
}
