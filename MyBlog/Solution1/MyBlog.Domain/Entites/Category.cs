namespace MyBlog.Domain.Entites;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    //Navigation properties
    public ICollection<Subcategory> Subcategories { get; set; }
    public ICollection<Article> Articles { get; set; }
}