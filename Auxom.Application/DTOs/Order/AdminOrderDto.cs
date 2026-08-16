using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.DTOs.Order
{
    public  class AdminOrderDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int ItemCount { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } 
        public DateTime OrderDate { get; set; } 
    }
}
