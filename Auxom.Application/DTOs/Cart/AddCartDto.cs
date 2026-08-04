using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Cart
{
    public class AddCartDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
