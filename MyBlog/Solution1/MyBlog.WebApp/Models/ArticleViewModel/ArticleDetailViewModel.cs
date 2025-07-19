namespace WebApp.Models.ArticleViewModel;

public class ArticleDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string AuthorNickName { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CommentViewModel> Comments { get; set; }
}

public class CommentViewModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string CommenterNickName { get; set; }
    public DateTime CreatedAt { get; set;}
}