namespace MyBlog.Application.Dtos.UserDtos;

public class ResultUserDto
{
    public string Id { get; set; }
    public string? NickName { get; set; }
    public string Email { get; set; }
    public string? Job { get; set; }
    public string? About { get; set; }
    public bool IsProfileCompleted { get; set; }
    public int ArticleCount { get; set; }
    public int CommentCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
   // public string? FeaturedImageUrl { get; set; }
    public string? Summary { get; set; }
}