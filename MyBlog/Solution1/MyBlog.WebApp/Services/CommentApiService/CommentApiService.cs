using WebApp.Models.CommentViewModel;

namespace WebApp.Services.CommentApiService;

public class CommentApiService : ICommentApiService
{
    private readonly HttpClient _client;

    public CommentApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<(bool Success, string ErrorMessage)> AddCommentAsync(CreateCommentViewModel createCommentViewModel)
    {
        var response = await _client.PostAsJsonAsync("/api/Comment", createCommentViewModel);
        if (response.IsSuccessStatusCode)
            return (true, string.Empty);
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        return (false, errorMessage);
    }

    public async Task<(bool Success, string ErrorMessage)> DeleteCommentAsync(int commentId)
    {
        var response = await _client.DeleteAsync($"/api/Comment/{commentId}");
        if (response.IsSuccessStatusCode)
            return (true, string.Empty);
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        return (false, errorMessage);
    }

    public async Task<List<ResultCommentViewModel>> GetCommentsByArticleAsync(int articleId)
    {
        try
        {
            var response = await _client.GetAsync($"/api/Comment/article/{articleId}");
            if (response.IsSuccessStatusCode)
            {
                var comments = await response.Content.ReadFromJsonAsync<List<ResultCommentViewModel>>();
                return comments ?? new List<ResultCommentViewModel>();
            }
            return new List<ResultCommentViewModel>(); // Hata durumunda boş liste dön
        }
        catch
        {
            return new List<ResultCommentViewModel>(); // Exception durumunda boş liste dön
        }
    }

    public async Task<(bool Success, string ErrorMessage)> UpdateCommentAsync(UpdateCommentViewModel updateCommentViewModel)
    {
        var response = await _client.PutAsJsonAsync("/api/Comment", updateCommentViewModel);
        if (response.IsSuccessStatusCode)
            return (true, string.Empty);
        
        var errorMessage = await response.Content.ReadAsStringAsync();
        return (false, errorMessage);
    }
}