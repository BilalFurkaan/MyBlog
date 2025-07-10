namespace MyBlog.Application.Dtos.UserDtos;

public class ResultUserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Job { get; set; }
    public string About { get; set; }
    public int ArticleCount { get; set; }
    public int CommentCount { get; set; }
}