namespace WebApp.Models.ArticleViewModel;

public class ArticleDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string AuthorNickName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ViewCount { get; set; }
    public string CategoryName { get; set; }
    public string SubcategoryName { get; set; }
    public string TechnologyName { get; set; }
    public List<CommentViewModel> Comments { get; set; }
}

public class CommentViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    public string CommenterNickName { get; set; }
    public bool IsMine { get; set; }
    public DateTime CreatedAt { get; set;}
}