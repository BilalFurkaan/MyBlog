namespace MyBlog.Application.Dtos.SubcategoryDtos;

public class UpdateSubcategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
}