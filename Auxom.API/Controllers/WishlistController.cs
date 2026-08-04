using Auxom.Application.DTOs.Wishlist;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auxom.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController:ControllerBase
    {

        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        [HttpPost]
        public async Task<IActionResult> AddToWishlistAsync(CreateWishlistDto dto)
        {
            var userId = GetUserId();
            await _wishlistService.AddToWishlistAsync(userId, dto);

            return Created(string.Empty, new
            {
                Message = "Product added successfully to wishlist."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlistAsync()
        {
            var userId = GetUserId();

            var wishlist = await _wishlistService.GetWishlistByUserIdAsync(userId);
        
            return Ok(wishlist);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveWishlistAsync(Guid productId)
        {

            var userId = GetUserId();

            await _wishlistService.RemoveWishlistAsync(userId, productId);
            return Ok(new
            {
                Message = "Product removed from wishlist."
            });
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
