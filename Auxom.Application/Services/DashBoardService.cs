using Auxom.Application.DTOs.DashBoard;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        public DashBoardService(IProductRepository productRepository , IUserRepository userRepository , IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;

        }
        public async Task<DashBoardDto> GetDashBoardAsync()
        {
            var TotalProducts = await _productRepository.GetTotalProductsAsync();
            var TotalOrders = await _orderRepository.GetTotalOrdersAsync();
            var TotalUsers = await _userRepository.GetTotalUsersAsync();
            var TotalRevenue = await _orderRepository.GetTotalRevenueAsync();

            var orders = await _orderRepository.GetOrdersAsync();

            var CurrentYear = DateTime.UtcNow.Year;
            var MonthlySales = orders.Where(o => o.Status == "Delivered").
                GroupBy(o => o.OrderDate.Month).
                Select(g => new MonthlySalesDto
                {
                    MonthNumber = g.Key,
                    Month = new DateTime(CurrentYear, g.Key, 1).ToString("MMMM"),
                    Sales = g.Sum(o => o.Total)
                }).OrderBy(x =>x.MonthNumber).ToList();

            return new DashBoardDto
            {
                TotalProducts = TotalProducts,
                TotalOrders = TotalOrders,
                TotalUsers = TotalUsers,
                TotalRevenue = TotalRevenue,
                MonthlySales = MonthlySales



            };

        }
     
    }
}
