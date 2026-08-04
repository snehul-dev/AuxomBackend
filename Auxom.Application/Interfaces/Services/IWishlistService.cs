using Auxom.Application.DTOs.Wishlist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IWishlistService
    {
        Task AddToWishlistAsync(Guid userId, CreateWishlistDto dto);
        Task<IEnumerable<WishlistDto>> GetWishlistByUserIdAsync(Guid userId);
        Task RemoveWishlistAsync(Guid userId , Guid productId);
    }
}
