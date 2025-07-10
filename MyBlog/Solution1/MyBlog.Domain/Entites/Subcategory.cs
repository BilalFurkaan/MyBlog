namespace MyBlog.Domain.Entites;

public class Subcategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    // Navigation
    public Category Category { get; set; }
    public ICollection<Technology> Technologies { get; set; }
    public ICollection<Article> Articles { get; set; }
}