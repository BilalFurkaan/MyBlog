using System.ComponentModel.DataAnnotations;

namespace MyBlog.Application.Dtos.UserDtos;

public class ChangePassword
{
    [Required]
    public string Id { get; set; }

    [StringLength(100, MinimumLength = 6)]
    public string OldPassword { get; set; }
    
    [StringLength(100, MinimumLength = 6)]
    public string? NewPassword { get; set; }
    
    [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
    public string? ConfirmNewPassword { get; set; }
}