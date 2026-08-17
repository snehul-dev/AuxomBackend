using Auxom.Application.DTOs.Review;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Auxom.Application.Exceptions;
using AutoMapper;
using Auxom.Domain.Entities;


namespace Auxom.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository reviewRepository , IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        public async Task AddReviewAsync(Guid userId, Guid productId, CreateReviewDto dto)
        {
            var existingReview = await _reviewRepository.GetByUserIdAndProductIdAsync(userId, productId);
            if(existingReview != null)
            {
                throw new BadRequestException("Already review the product");
            }
            var review = _mapper.Map<Review>(dto);
            review.UserId = userId;
            review.ProductId = productId;

            await _reviewRepository.AddReviewAsync(review);
            await _reviewRepository.SaveChangesAsync();
        }

        public async Task<List<ReviewResponseDto>> GetByProductAsync(Guid productId)
        {
            var reviews =  await _reviewRepository.GetByProductIdAsync(productId);
            return _mapper.Map<List<ReviewResponseDto>>(reviews);
        }
    }
}
