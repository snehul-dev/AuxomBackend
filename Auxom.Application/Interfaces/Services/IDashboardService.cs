using Auxom.Application.DTOs.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IDashBoardService
    {
        Task<DashBoardDto> GetDashBoardAsync();
    }
}
