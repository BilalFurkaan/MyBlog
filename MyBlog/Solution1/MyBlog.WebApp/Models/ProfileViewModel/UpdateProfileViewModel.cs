using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ProfileViewModel;

public class UpdateProfileViewModel
{
    [Required]
    public string Id { get; set; }
    
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Takma ad 2-50 karakter arasında olmalıdır")]
    [Display(Name = "Takma Ad")]
    public string? NickName { get; set; }
    
    [StringLength(100, ErrorMessage = "Meslek en fazla 100 karakter olabilir")]
    [Display(Name = "Meslek")]
    public string? Job { get; set; }
    
    [StringLength(500, ErrorMessage = "Hakkımda en fazla 500 karakter olabilir")]
    [Display(Name = "Hakkımda")]
    public string? About { get; set; }
    
}