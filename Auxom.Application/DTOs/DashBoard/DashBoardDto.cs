using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.DashBoard
{
    public class DashBoardDto
    {
        public int  TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public decimal TotalRevenue { get; set; }
        
    }
}
