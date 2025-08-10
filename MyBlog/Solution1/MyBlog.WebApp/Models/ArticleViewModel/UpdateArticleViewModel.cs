using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models.ArticleViewModel;

public abstract class UpdateArticleViewModel
{
    public int Id { get; set; }
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
    public bool IsOwner { get; set; }
    public List<SelectListItem> Categories { get; set; } = new();
    public List<SelectListItem> Subcategories { get; set; } = new();
    public List<SelectListItem> Technologies { get; set; } = new();
}