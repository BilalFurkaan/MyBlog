using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.CommentDtos;

public class UpdateCommentDto
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(1000, MinimumLength = 10)]
    public string Content { get; set; }
    
    public string UserId { get; set; }
}