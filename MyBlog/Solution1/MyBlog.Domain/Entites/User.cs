namespace MyBlog.Domain.Entites;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string NickName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Job { get; set; }
    public string About { get; set; }
    // Navigation
    public ICollection<Article> Articles { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
    