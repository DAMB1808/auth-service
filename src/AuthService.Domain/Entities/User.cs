using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Entities.User;

public class User
{
    [Key]
    [MaxLength(16)]
    public string Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(25)]
    public string Name { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MaxLength(25)]
    public string Surname { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLenght(255)]
    public string Password { get; set; }

    public bool Status { get; set; }

    [Required]
    public DataTime CreatedAt { get; set; }

    [Required]
    public DataTime UpdateAt { get; set; }

    public UserProfile Profile { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public UserEmail UserEmail { get; set; } = null!;
    public UserPasswordReset PasswordReset { get; set; } = null!;
    
}