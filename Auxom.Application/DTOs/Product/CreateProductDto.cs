using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public bool InStock { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        public string Description { get; set; } = string.Empty;
    }
}
