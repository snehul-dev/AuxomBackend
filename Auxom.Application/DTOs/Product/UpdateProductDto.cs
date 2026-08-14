using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Color { get; set; } = string.Empty;
        public bool InStock { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
