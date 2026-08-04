using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Cart
{
    public class CartItemDto
    {
        public Guid CartItemId { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

    }
}
