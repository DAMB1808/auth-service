using AuthService.Application.Service;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User> GetByIdAsync(string id)
    {
        var user = await context.Users
        .Include(u => u.UserProfile)
        .Include(u => u.UserEmail)
        .Include(u => u.UserPasswordReset)
        .Include(u => u.UserRoles)
        .FirstOrDefaultAsync(u => u.Id == id);
        
        return user;
    }
}