using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.CommentDtos;

public class CreateCommentDto
{
 [Required]
 [StringLength(1000, MinimumLength = 10)]
 public string Content { get; set; }
 [Required]
 public int ArticleId { get; set; }
}