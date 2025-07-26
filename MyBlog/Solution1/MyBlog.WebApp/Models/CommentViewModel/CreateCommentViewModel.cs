using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.CommentViewModel;

public class CreateCommentViewModel
{
    [Required]
    [StringLength(1000, MinimumLength = 10)]
    public string Content { get; set; }
    
    [Required]
    public int ArticleId { get; set; }
 
    public string? UserId { get; set; } // Backend'de JWT'den alınacak, form'dan gelmeyecek
}