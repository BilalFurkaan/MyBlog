using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ProfileViewModel;

public class ChangePasswordViewModel
{
    [Required]
    public string Id { get; set; }
  
    [Required(ErrorMessage = "Mevcut şifre gereklidir")]
    [StringLength(100, MinimumLength = 6)]
    public string OldPassword { get; set; } 
    
    [Required(ErrorMessage = "Yeni şifre gereklidir")]
    [StringLength(100, MinimumLength = 6)]
    public string? NewPassword { get; set; }
    
    [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
    public string? ConfirmNewPassword { get; set; }
}