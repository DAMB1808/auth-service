using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Domain.Entities;

public class UserProfile
{
    [Key]
    [MaxLength(16)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [MaxLength(16)]
    [ForeingKey(nameof(User))]
    public string UserId { get; set; } = string.Empty; 

    public string ProfilePictureUrl { get; set; }
    public string Bio { get; set; }
    public DataTime DataOfBirth { get; set; }

    public User User { get; set; } = null!;
}