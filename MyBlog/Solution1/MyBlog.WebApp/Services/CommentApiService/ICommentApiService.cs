using WebApp.Models.CommentViewModel;

namespace WebApp.Services.CommentApiService;

public interface ICommentApiService
{
    Task<(bool Success, string ErrorMessage)> AddCommentAsync(CreateCommentViewModel createCommentViewModel);
    Task<(bool Success, string ErrorMessage)> DeleteCommentAsync(int commentId);
    Task<List<ResultCommentViewModel>> GetCommentsByArticleAsync(int articleId);
    Task<(bool Success, string ErrorMessage)> UpdateCommentAsync(UpdateCommentViewModel updateCommentViewModel);
}
