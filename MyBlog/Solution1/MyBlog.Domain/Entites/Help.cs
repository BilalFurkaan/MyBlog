namespace MyBlog.Domain.Entites;

public class Help
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public bool Status { get; set; }
}