using Auxom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auxom.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(Guid id);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task AddUserAsync(User user);

        Task UpdateUserAsync(User updateduser);

        Task<bool> DeleteUserAsync(Guid id);

        Task SaveChangesAsync();
    }
}
