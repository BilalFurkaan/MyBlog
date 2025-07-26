using WebApp.Models.LoginViewModel;
using WebApp.Models.ProfileViewModel;

namespace WebApp.Services.UserApiService;

public interface IUserApiService
{
    Task<(bool Success, string ErrorMessage, string NickName, string UserId)> LoginAsync(LoginViewModel model);
    Task<(bool Success, string ErrorMessage, string NickName, string UserId, string Token)> LoginAsyncWithToken(LoginViewModel model);
    Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model);
    Task<(bool Success, string ErrorMessage)> CompleteProfileAsync(CompleteProfileViewModel model);
    Task<(bool Success, string ErrorMessage)> UpdateProfileAsync(UpdateProfileViewModel model);
    Task<(bool Success, string ErrorMessage, UserProfileViewModel Profile)> GetUserProfileAsync(string userId);
    Task<(bool Success, string ErrorMessage)> ChangePasswordAsync(ChangePasswordViewModel model);
 
}