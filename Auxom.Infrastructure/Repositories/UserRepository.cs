using Auxom.Domain.Entities;
using Auxom.Domain.Interfaces;
using Auxom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly AuxomContext _context;
        public UserRepository(AuxomContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
           return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
           
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateUserAsync(User updateduser)
        {
            _context.Users.Update(updateduser);
            return Task.CompletedTask;
        }
    }
}
