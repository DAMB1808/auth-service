using AuthService.Domain.Entities;
 
namespace AuthService.Domain.Interfaces;
 
public interface IUserRepository
{
    // Crea un nuevo usuario en la base de datos
    Task<User> CreateAsync(User user);

    // Obtiene un usuario por su ID
    Task<User?> GetByIdAsync(string id);

    // Obtiene un usuario por su correo electrónico
    Task<User?> GetByEmailAsync(string email);

    // Obtiene un usuario por su nombre de usuario
    Task<User?> GetByUsernameAsync(string username);

    // Obtiene un usuario por su token de verificación de email
    Task<User?> GetByEmailVerificationTokenAsync(string token);

    // Obtiene un usuario por su token de recuperación de contraseña
    Task<User?> GetByPasswordResetTokenAsync(string token);

    // Verifica si existe un usuario con el correo especificado
    Task<bool> ExistsByEmailAsync(string email);

    // Verifica si existe un usuario con el nombre de usuario especificado
    Task<bool> ExistsByUsernameAsync(string username);

    // Actualiza la información de un usuario existente
    Task<User> UpdateAsync(User user);

    // Elimina un usuario por su ID
    Task<bool> DeleteAsync(string id);

    // Actualiza el rol asignado a un usuario
    Task UpdateUserRoleAsync(string userId, string roleId);
}
 