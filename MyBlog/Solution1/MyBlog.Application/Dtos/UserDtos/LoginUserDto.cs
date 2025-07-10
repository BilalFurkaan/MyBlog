using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.UserDtos;

public class LoginUserDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
} 