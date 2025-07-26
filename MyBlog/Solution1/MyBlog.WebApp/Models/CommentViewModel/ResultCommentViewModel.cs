namespace WebApp.Models.CommentViewModel;

public class ResulCommentViewModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ArticleId { get; set; }
    public string ArticleTitle { get; set; }
    public string UserId { get; set; }
    public string CommenterNickName { get; set; }
}