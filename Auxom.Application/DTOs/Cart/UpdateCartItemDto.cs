using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Cart
{
    public class UpdateCartItemDto
    {
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
