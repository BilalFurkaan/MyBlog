using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ArticleViewModel;

public class CreateArticleViewModel
{
    [Required]
    [StringLength(200, MinimumLength = 10)]
    public string Title { get; set; }
    [Required]
    [StringLength(10000, MinimumLength = 100)]
    public string Content { get; set; }

    public int? CategoryId { get; set; }
    public int? SubcategoryId { get; set; }
    public int? TechnologyId { get; set; }
    public string UserId { get; set; }
}