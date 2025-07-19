using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.UserDtos;

public class UpdateUserDto
{
    [Required]
    public string Id { get; set; }
    
    [StringLength(50, MinimumLength = 2)]
    public string? NickName { get; set; }
    
    [StringLength(100)]
    public string? Job { get; set; }
    
    [StringLength(500)]
    public string? About { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [StringLength(100, MinimumLength = 6)]
    public string? NewPassword { get; set; }
    
    [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
    public string? ConfirmNewPassword { get; set; }
}