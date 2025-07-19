namespace WebApp.Models.ArticleViewModel;

public class ArticleListViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string AuthorNickName { get; set; }
    public string CategoryName { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CommentCount { get; set; }
}