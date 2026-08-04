using Auxom.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(Guid userid);
        Task AddCartAsync(Guid userid ,AddCartDto dto);
        Task<bool> UpdateQuantityAsync(Guid cartitemid, int quantity);
        Task<bool> RemoveCartItemAsync(Guid cartitemid);
        Task ClearCartAsync(Guid userId);
    }
}
