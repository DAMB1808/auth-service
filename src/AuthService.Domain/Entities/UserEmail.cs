using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuthService.Domain.Entities;

public class UserEmail
{
    [Key]
    [MaxLength(16)]
    public string Id {get; set;} = string.Empty;

    [Required]
    [MaxLength(16)]
    [ForeignKey(nameof(User))]
    public string UserId {get; set;} = string.Empty;

    [Required]
    public bool EmailVerified { get; set; } = false;

    [Required]
    public string EmailVerificationToken {get; set;} = string.Empty;

    [Required]
    public DateTime EmailVerificationTokenExpiresAt {get; set;} = DateTime.UtcNow;

    public User User {get; set;} = null!;
}

/*
UserEmail
+------------------+------------------+---------------------------+--------------------------+-----------------------------------+---------------------+
| Id               | UserId           | EmailVerefied             | EmailVerificationToken   | EmailVerificationTokenExpiresAt   | User                |
+------------------+------------------+---------------------------+--------------------------+-----------------------------------+---------------------+
| EMAIL_001        | USER_001         | true                      | abc123                   | 2022-01-01 00:00:00               | USER_001            |
| EMAIL_002        | USER_002         | false                     | def456                   | 2022-01-01                        | USER_002            |
+------------------+------------------+---------------------------+--------------------------+-----------------------------------+---------------------+
*/