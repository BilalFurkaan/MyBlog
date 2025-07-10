namespace MyBlog.Domain.Entites;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CategoryId { get; set; }
    public int? SubcategoryId { get; set; }
    public int? TechnologyId { get; set; }
    public int UserId { get; set; }
    //navigation
    public Category Category { get; set; }
    public Subcategory Subcategory { get; set; }
    public Technology Technology { get; set; }
    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; }
}