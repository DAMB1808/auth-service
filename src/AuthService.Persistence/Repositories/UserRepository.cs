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

    public async Task<User> CreateAsync(User user)
    {
        context. Users.Add(user);
        await context. SaveChangesAsync();
        return await GetByIdAsync(user.Id);
    }
    // 7. Actualiza la información de un usuario existente
    public async Task<User> UpdateAsync(User user)
    {
        await context. SaveChangesAsync();
        return await GetByIdAsync(user.Id);
    }

    // 8. Elimina un usuario de la base de datos por su ID
    public async Task<bool> DeleteAsync(string id)
    {
        var user = await GetByIdAsync(id);
        context. Users. Remove(user);
        await context. SaveChangesAsync();
        return true;
    }

}