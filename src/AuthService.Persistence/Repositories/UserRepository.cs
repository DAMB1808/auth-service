using AuthService.Application.Service;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
// 2. Busca un usuario por su Email (sin importar mayúsculas/minúsculas)
public async Task<User?> GetByEmailAsync(string email)
{
    return await context.Users
        .Include(u => u.UserProfile)
        .Include(u => u.UserEmail)
        .Include(u => u.UserPasswordReset)
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Email, email));
}

// 3. Busca un usuario por su Username (sin importar mayúsculas/minúsculas)
public async Task<User?> GetByUsernameAsync(string username)
{
    return await context.Users
        .Include(u => u.UserProfile)
        .Include(u => u.UserEmail)
        .Include(u => u.UserPasswordReset)
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Username, username));
}

// 4. Busca un usuario mediante su token de verificación de correo
public async Task<User?> GetByEmailVerificationTokenAsync(string token)
{
    return await context.Users
        .Include(u => u.UserProfile)
        .Include(u => u.UserEmail)
        .Include(u => u.UserPasswordReset)
        .Include(u => u.UserRoles)
        .ThenInclude(ur => ur.Role)
        .FirstOrDefaultAsync(u => u.UserEmail != null && u.UserEmail.EmailVerificationToken == token);
}
}