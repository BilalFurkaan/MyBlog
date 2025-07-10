using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.UserDtos;

public class UpdateUserDto
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string NickName { get; set; }
    
    public string? Password { get; set; }  // Optional - only if user wants to change
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Job { get; set; }
    
    [Required]
    [StringLength(500)]
    public string About { get; set; }
}