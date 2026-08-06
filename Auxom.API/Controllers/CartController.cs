using Auxom.Application.DTOs.Cart;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Pkcs;

namespace Auxom.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController:ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddCartDto dto)
        {
            var userId = GetUserId();

            await _cartService.AddCartAsync(userId, dto);

            return Created(string.Empty, new
            {
                message = "Product added to cart successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();

            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);

        }

        [HttpPut("items/{CartItemId}")]
        public async Task<IActionResult> UpdateQuantity(Guid CartItemId , UpdateCartItemDto dto)
        {
            var updated = await _cartService.UpdateQuantityAsync(CartItemId, dto.Quantity);
        

            return NoContent();
        }

        [HttpDelete("items/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(Guid cartItemId)
        {
             await _cartService.RemoveCartItemAsync(cartItemId);

       
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {

             var userId = GetUserId();
            await _cartService.ClearCartAsync(userId);

            return NoContent();

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
