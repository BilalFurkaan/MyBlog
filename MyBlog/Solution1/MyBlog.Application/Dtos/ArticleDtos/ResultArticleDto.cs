namespace MyBlog.Application.Dtos.ArticleDtos;

public class ResultArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int? SubcategoryId { get; set; }
    public string? SubcategoryName { get; set; }
    public int? TechnologyId { get; set; }
    public string? TechnologyName { get; set; }
    public string UserId { get; set; }
    public string AuthorNickName { get; set; }
    public int CommentCount { get; set; }
}