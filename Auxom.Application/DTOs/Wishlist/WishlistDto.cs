using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Wishlist
{
    public class WishlistDto
    {
        public Guid WishlistId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool InStock { get; set; }
        public double Rating { get; set; }
    }
}
