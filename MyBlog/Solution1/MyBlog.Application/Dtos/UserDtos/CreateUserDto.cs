using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.UserDtos;

public class CreateUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }  // Use Identity validation 
    

    
}