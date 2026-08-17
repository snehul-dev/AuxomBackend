using Auxom.Application.DTOs.Review;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auxom.API.Controllers.User
{
  
    [ApiController]
    [Route("api/review")]
    public class ReviewController:ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost("{productId}")]
        [Authorize]
        public async Task<IActionResult> AddReviewAsync(Guid productId , CreateReviewDto dto)
        {
            var userId = GetUserId();
            await _reviewService.AddReviewAsync(userId, productId, dto);
            return Ok(new
            {
                Message = "Review added succesfully"
            });
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductIdAsync(Guid productId)
        {
            var reviews = await _reviewService.GetByProductAsync(productId);
            return Ok(reviews);
        }
        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return Guid.Parse(userId);
        }


    }
}
