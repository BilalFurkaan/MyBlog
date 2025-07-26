using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.CommentViewModel;

public class UpdateCommentViewModel
{
    [Required]
    [StringLength(1000, MinimumLength = 10)]
    public string Content { get; set; }
    [Required]
    public int ArticleId { get; set; }
 
    public string UserId { get; set; }
}