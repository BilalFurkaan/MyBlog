namespace MyBlog.Domain.Entites;

public class Technology
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SubcategoryId { get; set; }
    // Navigation
    public Subcategory Subcategory { get; set; }
    public ICollection<Article> Articles { get; set; }
}