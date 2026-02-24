using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuthService.Domain.Enums;

namespace AuthService.Domain.Entities;

public class User
{
    [Key]
    [MaxLength (16)]
    public string Id {get; set;} = string.Empty;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(50)]
    public string Name {get; set;} = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MaxLength(50)]
    public string Surname {get; set;} = string.Empty;

    [Required]
    [MaxLength(50)]    
    public string Username {get; set;} = string.Empty;

    [Required]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Password {get; set;} = string.Empty;

    
    public string Status {get; set;} = string.Empty;

    [Required]
    public DateTime CreatedAt {get; set;}

    [Required]
    public DateTime UpdatedAt {get; set;}

    // Relaciones de navegacion solo dentro del codigo
    // Esto no altera la base de datos
    public UserProfile UserProfile  {get; set;} = null!; // El ! indica que la propiedad no sera null
    public ICollection<UserRole> UserRoles {get; set;} = new List<UserRole>(); // Indica que la propiedad es una coleccion de UserRole
    public UserEmail UserEmail {get; set;} = null!;
    public UserPasswordReset UserPasswordReset  {get; set;} = null!;
}

/*
User
+----------+----------+----------+----------+---------------------------+------------+--------+---------------------+---------------------+
| Id       | Name     | Surname  | Username | Email                     | Password   | Status | CreatedAt           | UpdatedAt           |
+----------+----------+----------+----------+---------------------------+------------+--------+---------------------+---------------------+
| USER_001 | Mateo    | Doe      | johndoe  | [EMAIL_ADDRESS]           | [PASSWORD] | ACTIVE | 2022-01-01 00:00:00 | 2022-01-01 00:00:00 |
| USER_002 | Mynor    | Smith    | janesmith| [EMAIL_ADDRESS]           | [PASSWORD] | ACTIVE | 2022-01-01 00:00:00 | 2022-01-01 00:00:00 |
+------------------+------------------+------------------+---------------------------+----------------------+------------+---------------------+---------------------+
*/