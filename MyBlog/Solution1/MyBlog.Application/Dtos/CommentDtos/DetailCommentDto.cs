namespace MyBlog.Application.Dtos.CommentDtos;

public class DetailCommentDto : ResultCommentDto
{
    public int ArticleId { get; set; }
    public string ArticleTitle { get; set; }
    public int UserId { get; set; }
    public string CommenterUserName { get; set; }
    public string CommenterJob { get; set; }
}