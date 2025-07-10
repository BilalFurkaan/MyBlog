using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.UserDtos;

public class CreateUserDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string UserName { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string NickName { get; set; }
    
    [Required]
    public string Password { get; set; }  // Use Identity validation 
    
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