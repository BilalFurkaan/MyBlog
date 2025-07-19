using WebApp.Models.LoginViewModel;
using WebApp.Models.ProfileViewModel;

namespace WebApp.Services.UserApiService;

public class UserApiService: IUserApiService
{
    private readonly HttpClient _httpClient;

    public UserApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(bool Success, string ErrorMessage, string NickName, string UserId)> LoginAsync(LoginViewModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/User/login", model);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var obj = System.Text.Json.JsonDocument.Parse(json).RootElement;
            var nickName = obj.TryGetProperty("nickName", out var n) ? n.GetString() : null;
            var userId = obj.TryGetProperty("userId", out var u) ? u.GetString() : null;
            return (true, string.Empty, nickName, userId);
        }
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        return (false, errorMessage, null, null);
    }

    public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/User/register", model);
        if (response.IsSuccessStatusCode)
        {
            return (true, string.Empty);
        }
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        return (false, errorMessage);
    }

    public async Task<(bool Success, string ErrorMessage)> CompleteProfileAsync(CompleteProfileViewModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/User/complete-profile", model);
        if (response.IsSuccessStatusCode)
        {
            return (true, string.Empty);
        }
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        return (false, errorMessage);
    }

    public async Task<(bool Success, string ErrorMessage)> UpdateProfileAsync(UpdateProfileViewModel model)
    {
        var response = await _httpClient.PutAsJsonAsync("/api/User/update", model);
        if (response.IsSuccessStatusCode)
        {
            return (true, string.Empty);
        }
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        return (false, errorMessage);
    }

    public async Task<(bool Success, string ErrorMessage, UserProfileViewModel Profile)> GetUserProfileAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"/api/User/profile/{userId}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var obj = System.Text.Json.JsonDocument.Parse(json).RootElement;
            
            var profile = new UserProfileViewModel
            {
                Id = obj.TryGetProperty("id", out var id) ? id.GetString() : userId,
                Email = obj.TryGetProperty("email", out var email) ? email.GetString() : "",
                NickName = obj.TryGetProperty("nickName", out var nickName) ? nickName.GetString() : "",
                Job = obj.TryGetProperty("job", out var job) ? job.GetString() : "",
                About = obj.TryGetProperty("about", out var about) ? about.GetString() : "",
                IsProfileCompleted = obj.TryGetProperty("isProfileCompleted", out var isCompleted) ? isCompleted.GetBoolean() : false
            };
            
            return (true, string.Empty, profile);
        }
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        return (false, errorMessage, null);
    }
}