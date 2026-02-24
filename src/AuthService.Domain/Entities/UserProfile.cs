using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Domain.Entities;

public class UserProfile
{
    [Key]
    [MaxLength(16)]
    public string Id {get; set;} = string.Empty;
    
    [Required]
    [MaxLength(16)]
    [ForeignKey(nameof(User))]
    public string UserId {get; set;} = string.Empty;

    public string? ProfilePictureUrl {get; set;}

    public string? Bio {get; set;}

    public DateTime? DateOfBirth {get; set;}


    public User User {get; set;} = null!;

}

/*
UserProfile
+------------------+------------------+---------------------------+------------------+---------------------+---------------------+
| Id               | UserId           | ProfilePictureUrl         | Bio              | DateOfBirth         | User                |
+------------------+------------------+---------------------------+------------------+---------------------+---------------------+
| PROFILE_001      | USER_001         | https://example.com/pic1  | Bio de John      | 1990-01-01          | USER_001            |
| PROFILE_002      | USER_002         | https://example.com/pic2  | Bio de Jane      | 1992-02-02          | USER_002            |
+------------------+------------------+---------------------------+------------------+---------------------+---------------------+
*/
