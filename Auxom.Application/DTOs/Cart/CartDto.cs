using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Cart
{
    public class CartDto
    {
     
        public Guid CartId { get; set; }
        public decimal GrandTotal { get; set; }
        public List<CartItemDto> Items { get; set; } = new();

        
    }
}
