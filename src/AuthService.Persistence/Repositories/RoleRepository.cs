using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementación del método GetByNameAsync
        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.Name == roleName);
        }

        // Implementación del método CountUsersInRoleAsync
        public async Task<int> CountUsersInRoleAsync(string roleId)
        {
            return await _context.UserRoles
                .Where(ur => ur.RoleId == roleId)  // Corregir el filtro para que use roleId
                .CountAsync();
        }

        // Implementación del método GetUsersInRoleAsync
        public async Task<IReadOnlyList<User>> GetUsersInRoleAsync(string roleId)
        {
            return await _context.UserRoles
                .Where(ur => ur.RoleId == roleId)  // Filtrar por RoleId
                .Select(ur => ur.User)
                .Include(u => u.UserProfile)
                .Include(u => u.UserEmail)
                .Include(u => u.UserRoles)
                .ToListAsync()
                .ContinueWith(t => (IReadOnlyList<User>)t.Result);
        }

        // Implementación del método GetUserRoleNamesAsync
        public async Task<IReadOnlyList<string>> GetUserRoleNamesAsync(string userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name)  // Suponiendo que tienes la propiedad Name en Role
                .ToListAsync()
                .ContinueWith(t => (IReadOnlyList<string>)t.Result);
        }
    }
}