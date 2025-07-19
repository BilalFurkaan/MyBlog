using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.LoginViewModel;

public class LoginViewModel
{
    [Required(ErrorMessage = "E-posta gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    [Display(Name = "E-posta")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Şifre gereklidir")]
    [Display(Name = "Şifre")]
    public string Password { get; set; }
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "E-posta gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    [Display(Name = "E-posta")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Şifre gereklidir")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
    [Display(Name = "Şifre")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Şifre tekrarı gereklidir")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
    [Display(Name = "Şifre Tekrarı")]
    public string ConfirmPassword { get; set; }
} 