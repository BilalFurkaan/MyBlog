namespace MyBlog.Application.Dtos.CommentDtos;

public class ResultCommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CommenterNickName { get; set; }
} 