namespace MyBlog.Application.Dtos.CommentDtos;

public class DetailCommentDto : ResultCommentDto
{
    public int ArticleId { get; set; }
    public string ArticleTitle { get; set; }
    public string UserId { get; set; }
    public string CommenterNickName { get; set; }
    public string CommenterJob { get; set; }
}