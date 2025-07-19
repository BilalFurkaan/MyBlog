using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.UserDtos;

public class CompleteProfileDto
{
    [Required]
    public string UserId { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string NickName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Job { get; set; }
    
    [Required]
    [StringLength(500)]
    public string About { get; set; }
} 