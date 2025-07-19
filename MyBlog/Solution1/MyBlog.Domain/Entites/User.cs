using Microsoft.AspNetCore.Identity;

namespace MyBlog.Domain.Entites;

public class User : IdentityUser
{
    public string? NickName { get; set; }
    public string? Job { get; set; }
    public string? About { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    // Navigation
    public ICollection<Article> Articles { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
    