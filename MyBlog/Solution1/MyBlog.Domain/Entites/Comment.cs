namespace MyBlog.Domain.Entites;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    // Navigation 
    public Article Article { get; set; }
    public User User { get; set; }
    
}