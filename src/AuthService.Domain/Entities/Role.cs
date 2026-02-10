using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Entities.Role;

public class Role
{
    [Key]
    [MaxLength(16)]
    public string Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; }

    // se va a utilizar para relacionarse con UserRole
    public ICollection<UserRole> UserRoles { get; set; }
}

/*Roles
+--------------+--------------+------------------+
| Id           | Name         | Description      |
+--------------+--------------+------------------+
| ADMIN        | Admin        | Administrador    |
| USER         | User         | Usuario normal   |
| GUEST        | Guest        | Invitado         |
+--------------+--------------+------------------+
 */