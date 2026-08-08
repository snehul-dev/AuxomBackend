using Auxom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Infrastructure.Data
{
    public class AdminSeeder
    {
        public static async Task SeedAdminAsync(AuxomContext context)
        {
            string AdminEmail = "admin@gmail.com";
            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == AdminEmail);

            if(existingUser != null)
            {
                return;
            }

            var admin = new User
            {
                FullName = "Admin",
                Email = AdminEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Admin",
                IsBlocked = false
            };

            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
                
                
        }
    }
}
