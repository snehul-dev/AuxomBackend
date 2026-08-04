using Auxom.Application.DTOs.Address;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auxom.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController:ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAddressAsync(CreateAddressDto dto)
        {
            var userId = GetUserId();

            await _addressService.AddAsync(userId , dto);
            return Created(string.Empty, new
            {
                Message = "Address added successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAddressesAsync()
        {
            var userId = GetUserId();

           var address =  await _addressService.GetAddressByUserAsync(userId);

            return Ok(address);

        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddressAsync(Guid addressId)
        {
            var userId = GetUserId();

            await _addressService.DeleteAsync(userId, addressId);
            return Ok(new
            {
                Message = "Address deleted successfully."
            });
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAsync(Guid addressId, UpdateAddressDto dto)
        {
            var userId = GetUserId();
            await _addressService.UpdateAsync(userId, addressId, dto);

            return Ok(new
            {
                Message = "Address updated successfully."
            });
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            return Guid.Parse(userId);
        }
    }
}
