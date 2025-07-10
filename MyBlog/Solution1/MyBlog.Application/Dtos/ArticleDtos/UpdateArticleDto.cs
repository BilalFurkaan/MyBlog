using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.ArticleDtos;

public class UpdateArticleDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 10)]
    public string Title { get; set; }

    [Required]
    [StringLength(10000, MinimumLength = 100)]
    public string Content { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public int? SubcategoryId { get; set; }
    public int? TechnologyId { get; set; }
}