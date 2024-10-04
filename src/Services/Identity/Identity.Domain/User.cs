using Microsoft.AspNetCore.Identity;
namespace Identity.Domain;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public DateTime? LastLoginTime { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    public Guid UserKey { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; }
    public string RefreshToken { get; set; }
}

