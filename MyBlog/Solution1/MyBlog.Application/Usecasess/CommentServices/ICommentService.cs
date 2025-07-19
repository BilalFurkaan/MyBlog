using MyBlog.Application.Dtos.ArticleDtos;
using MyBlog.Application.Dtos.CommentDtos;

namespace MyBlog.Application.Usecasess.CommentServices;

public interface ICommentService
{
    Task<IEnumerable<ResultCommentDto>> GetAllCommentsAsync();
    Task<ResultCommentDto> GetCommentByIdAsync(int id);
    Task<IEnumerable<ResultCommentDto>> GetCommentsByArticleIdAsync(int articleId);
    Task<ResultCommentDto> CreateCommentAsync(CreateCommentDto dto);
    Task<ResultCommentDto> UpdateCommentAsync(UpdateCommentDto dto);
    Task DeleteCommentAsync(int id);
}