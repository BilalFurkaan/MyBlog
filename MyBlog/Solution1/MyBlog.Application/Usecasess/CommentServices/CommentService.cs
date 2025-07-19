using MyBlog.Application.Dtos.CommentDtos;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entites;

namespace MyBlog.Application.Usecasess.CommentServices;

public class CommentService: ICommentService
{
    private readonly IRepository<Comment> _repository;

    public CommentService(IRepository<Comment> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ResultCommentDto>> GetAllCommentsAsync()
    {
        var comments=await _repository.GetAllWithIncludeAsync(c => c.Article, c => c.User);
        return comments.Select(c => new ResultCommentDto
        {
            Id = c.Id,
            Content = c.Content,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            ArticleId = c.ArticleId,
            UserId = c.UserId,
            ArticleTitle = c.Article?.Title ?? "Unknown Article",
            CommenterNickName = c.User?.NickName ?? "Anonymous"
        });
    }

    public async Task<ResultCommentDto> GetCommentByIdAsync(int id)
    {
        var comment = await _repository.GetByIdWithIncludeAsync(id, c => c.Article, c => c.User);
        if (comment == null)
        {
            throw new KeyNotFoundException($"Comment with ID {id} not found.");
        }
        return new ResultCommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            ArticleId = comment.ArticleId,
            UserId = comment.UserId,
            ArticleTitle = comment.Article?.Title ?? "Unknown Article",
            CommenterNickName = comment.User?.NickName ?? "Anonymous"
        };
    }

    public async Task<IEnumerable<ResultCommentDto>> GetCommentsByArticleIdAsync(int articleId)
    {
        var comments = await _repository.GetAllWithIncludeAsync(c => c.ArticleId == articleId, c => c.Article, c => c.User);
        return comments.Select(c => new ResultCommentDto
        {
            Id = c.Id,
            Content = c.Content,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            ArticleId = c.ArticleId,
            UserId = c.UserId,
            ArticleTitle = c.Article?.Title ?? "Unknown Article",
            CommenterNickName = c.User?.NickName ?? "Anonymous"
        });
    }

    public async Task<ResultCommentDto> CreateCommentAsync(CreateCommentDto dto)
    {
        var comment = new Comment
        {
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ArticleId = dto.ArticleId,
            UserId = dto.UserId
        };

        await _repository.AddAsync(comment);
        await _repository.SaveChangesAsync();

        // Eklendikten sonra navigation property'lerle tekrar çek
        var created = await _repository.GetByIdWithIncludeAsync(comment.Id, c => c.Article, c => c.User);

        return new ResultCommentDto
        {
            Id = created.Id,
            Content = created.Content,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt,
            ArticleId = created.ArticleId,
            UserId = created.UserId,
            ArticleTitle = created.Article?.Title ?? "Unknown Article",
            CommenterNickName = created.User?.NickName ?? "Anonymous"
        };
    }

    public async Task<ResultCommentDto> UpdateCommentAsync(UpdateCommentDto dto)
    {
        var comment = await _repository.GetByIdWithIncludeAsync(dto.Id, c => c.Article, c => c.User);
        if (comment == null)
            throw new KeyNotFoundException($"Comment with ID {dto.Id} not found.");

        comment.Content = dto.Content;
        comment.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(comment);
        await _repository.SaveChangesAsync();

        return new ResultCommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            ArticleId = comment.ArticleId,
            UserId = comment.UserId,
            ArticleTitle = comment.Article?.Title ?? "Unknown Article",
            CommenterNickName = comment.User?.NickName ?? "Anonymous"
        };
    }

    public async Task DeleteCommentAsync(int id)
    {
        var comment = await _repository.GetByIdAsync(id);
        if (comment == null)
        {
            throw new KeyNotFoundException($"Comment with ID {id} not found.");
        }

        await _repository.DeleteAsync(comment.Id);
        await _repository.SaveChangesAsync();
    }
}