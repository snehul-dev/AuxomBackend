using System;
using System.Collections.Generic;
using Auxom.Domain.Entities;
using System.Text;

namespace Auxom.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
