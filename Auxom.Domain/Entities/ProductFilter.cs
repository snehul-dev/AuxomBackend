using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Entities
{
    public class ProductFilter
    {
        public string? Search { get; set; }
        public string? Color { get; set; }
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? InStock { get; set; }
        public string? SortBy { get; set; }
    }
}
