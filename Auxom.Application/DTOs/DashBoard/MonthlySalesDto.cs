using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.DashBoard
{
    public class MonthlySalesDto
    {
        public string Month { get; set; } = string.Empty;
        public decimal Sales { get; set; }
    }
}
