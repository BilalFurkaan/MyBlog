using MyBlog.Application.Dtos.ArticleDtos;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entites;

namespace MyBlog.Application.Usecasess.ArticleServices;

public class ArticleService : IArticleService
{
    private readonly IRepository<Article> _repository;

    public ArticleService(IRepository<Article> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ResultArticleDto>> GetAllArticlesAsync()
    {
        var articles = await _repository.GetAllWithIncludeAsync(
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );

        return articles.Select(a => new ResultArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            CategoryId = a.CategoryId,
            CategoryName = a.Category?.Name ?? "Unknown Category",
            SubcategoryId = a.SubcategoryId,
            SubcategoryName = a.Subcategory?.Name ?? "Unknown Subcategory",
            TechnologyId = a.TechnologyId,
            TechnologyName = a.Technology?.Name ?? "Unknown Technology",
            UserId = a.UserId,
            AuthorNickName = a.User?.NickName ?? "Unknown Author",
            CommentCount = a.Comments?.Count ?? 0
        });
    }

    public async Task<ResultArticleDto> GetArticleByIdAsync(int id)
    {
        var article = await _repository.GetByIdWithIncludeAsync(
            id,
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );
        if (article == null)
            throw new KeyNotFoundException($"Article with ID {id} not found.");

        return new ResultArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            CreatedAt = article.CreatedAt,
            UpdatedAt = article.UpdatedAt,
            CategoryId = article.CategoryId,
            CategoryName = article.Category?.Name ?? "Unknown Category",
            SubcategoryId = article.SubcategoryId,
            SubcategoryName = article.Subcategory?.Name ?? "Unknown Subcategory",
            TechnologyId = article.TechnologyId,
            TechnologyName = article.Technology?.Name ?? "Unknown Technology",
            UserId = article.UserId,
            AuthorNickName = article.User?.NickName ?? "Unknown Author",
            CommentCount = article.Comments?.Count ?? 0
        };
    }

    public async Task<DetailArticleDto> GetArticleDetailAsync(int id)
    {
        var article = await _repository.GetByIdWithIncludeAsync(
            id,
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );
        if (article == null)
            throw new KeyNotFoundException($"Article with ID {id} not found.");

        return new DetailArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            CreatedAt = article.CreatedAt,
            UpdatedAt = article.UpdatedAt,
            CategoryId = article.CategoryId,
            CategoryName = article.Category?.Name ?? "Unknown Category",
            SubcategoryId = article.SubcategoryId,
            SubcategoryName = article.Subcategory?.Name ?? "Unknown Subcategory",
            TechnologyId = article.TechnologyId,
            TechnologyName = article.Technology?.Name ?? "Unknown Technology",
            UserId = article.UserId,
            AuthorNickName = article.User?.NickName ?? "Unknown Author",
            AuthorUserName = article.User?.UserName ?? "Unknown User",
            AuthorJob = article.User?.Job ?? "",
            AuthorAbout = article.User?.About ?? "",
            CommentCount = article.Comments?.Count ?? 0,
            Comments = article.Comments?.Select(c => new Dtos.CommentDtos.ResultCommentDto
            {
                Id = c.Id,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                CommenterNickName = c.User?.NickName ?? "Unknown"
            }).ToList() ?? new List<Dtos.CommentDtos.ResultCommentDto>()
        };
    }

    public async Task<IEnumerable<ResultArticleDto>> GetArticlesByCategoryIdAsync(int categoryId)
    {
        var articles = await _repository.GetWhereWithIncludeAsync(
            a => a.CategoryId == categoryId,
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );

        return articles.Select(a => new ResultArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            CategoryId = a.CategoryId,
            CategoryName = a.Category?.Name ?? "Unknown Category",
            SubcategoryId = a.SubcategoryId,
            SubcategoryName = a.Subcategory?.Name ?? "Unknown Subcategory",
            TechnologyId = a.TechnologyId,
            TechnologyName = a.Technology?.Name ?? "Unknown Technology",
            UserId = a.UserId,
            AuthorNickName = a.User?.NickName ?? "Unknown Author",
            CommentCount = a.Comments?.Count ?? 0
        });
    }

    public async Task<IEnumerable<ResultArticleDto>> GetArticlesBySubcategoryIdAsync(int subcategoryId)
    {
        var articles = await _repository.GetWhereWithIncludeAsync(
            a => a.SubcategoryId == subcategoryId,
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );

        return articles.Select(a => new ResultArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            CategoryId = a.CategoryId,
            CategoryName = a.Category?.Name ?? "Unknown Category",
            SubcategoryId = a.SubcategoryId,
            SubcategoryName = a.Subcategory?.Name ?? "Unknown Subcategory",
            TechnologyId = a.TechnologyId,
            TechnologyName = a.Technology?.Name ?? "Unknown Technology",
            UserId = a.UserId,
            AuthorNickName = a.User?.NickName ?? "Unknown Author",
            CommentCount = a.Comments?.Count ?? 0
        });
    }

    public async Task<IEnumerable<ResultArticleDto>> GetArticlesByTechnologyIdAsync(int technologyId)
    {
        var articles = await _repository.GetWhereWithIncludeAsync(
            a => a.TechnologyId == technologyId,
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );

        return articles.Select(a => new ResultArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            CategoryId = a.CategoryId,
            CategoryName = a.Category?.Name ?? "Unknown Category",
            SubcategoryId = a.SubcategoryId,
            SubcategoryName = a.Subcategory?.Name ?? "Unknown Subcategory",
            TechnologyId = a.TechnologyId,
            TechnologyName = a.Technology?.Name ?? "Unknown Technology",
            UserId = a.UserId,
            AuthorNickName = a.User?.NickName ?? "Unknown Author",
            CommentCount = a.Comments?.Count ?? 0
        });
    }
    
    public async Task<IEnumerable<ResultArticleDto>> GetArticlesByUserIdAsync(string userId)
    {
        var articles = await _repository.GetWhereWithIncludeAsync(
            a => a.UserId == userId,
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );

        return articles.Select(a => new ResultArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            CategoryId = a.CategoryId,
            CategoryName = a.Category?.Name ?? "Unknown Category",
            SubcategoryId = a.SubcategoryId,
            SubcategoryName = a.Subcategory?.Name ?? "Unknown Subcategory",
            TechnologyId = a.TechnologyId,
            TechnologyName = a.Technology?.Name ?? "Unknown Technology",
            UserId = a.UserId,
            AuthorNickName = a.User?.NickName ?? "Unknown Author",
            CommentCount = a.Comments?.Count ?? 0
        });
    }

    public async Task<PagedResult<ResultArticleDto>> GetPagedArticlesAsync(int page, int pageSize)
    {
        var articles = await _repository.GetAllWithIncludeAsync(
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );
        var totalCount = articles.Count();
        var pagedItems = articles
            .OrderByDescending(a => a.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new ResultArticleDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                CategoryId = a.CategoryId,
                CategoryName = a.Category?.Name ?? "Unknown Category",
                SubcategoryId = a.SubcategoryId,
                SubcategoryName = a.Subcategory?.Name ?? "Unknown Subcategory",
                TechnologyId = a.TechnologyId,
                TechnologyName = a.Technology?.Name ?? "Unknown Technology",
                UserId = a.UserId,
                AuthorNickName = a.User?.NickName ?? "Unknown Author",
                CommentCount = a.Comments?.Count ?? 0
            })
            .ToList();

        return new PagedResult<ResultArticleDto>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<ResultArticleDto> CreateArticleAsync(CreateArticleDto dto)
    {
        var article = new Article
        {
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CategoryId = dto.CategoryId,
            SubcategoryId = dto.SubcategoryId,
            TechnologyId = dto.TechnologyId,
            UserId = dto.UserId // UserId burada kimlik doğrulama sonrası atanmalı!
        };

        await _repository.AddAsync(article);
        await _repository.SaveChangesAsync();

        var createdArticle = await _repository.GetByIdWithIncludeAsync(
            article.Id,
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );

        return new ResultArticleDto
        {
            Id = createdArticle.Id,
            Title = createdArticle.Title,
            Content = createdArticle.Content,
            CreatedAt = createdArticle.CreatedAt,
            UpdatedAt = createdArticle.UpdatedAt,
            CategoryId = createdArticle.CategoryId,
            CategoryName = createdArticle.Category?.Name ?? "Unknown Category",
            SubcategoryId = createdArticle.SubcategoryId,
            SubcategoryName = createdArticle.Subcategory?.Name ?? "Unknown Subcategory",
            TechnologyId = createdArticle.TechnologyId,
            TechnologyName = createdArticle.Technology?.Name ?? "Unknown Technology",
            UserId = createdArticle.UserId,
            AuthorNickName = createdArticle.User?.NickName ?? "Unknown Author",
            CommentCount = createdArticle.Comments?.Count ?? 0
        };
    }

    public async Task<ResultArticleDto> UpdateArticleAsync(UpdateArticleDto dto)
    {
        var article = await _repository.GetByIdAsync(dto.Id);
        if (article == null)
            throw new KeyNotFoundException($"Article with ID {dto.Id} not found.");

        article.Title = dto.Title;
        article.Content = dto.Content;
        article.UpdatedAt = DateTime.UtcNow;
        article.CategoryId = dto.CategoryId;
        article.SubcategoryId = dto.SubcategoryId;
        article.TechnologyId = dto.TechnologyId;
        // UserId burada kimlik doğrulama sonrası atanmalı!

        await _repository.UpdateAsync(article);
        await _repository.SaveChangesAsync();

        var updatedArticle = await _repository.GetByIdWithIncludeAsync(
            article.Id,
            a => a.Category,
            a => a.Subcategory,
            a => a.Technology,
            a => a.User,
            a => a.Comments
        );

        return new ResultArticleDto
        {
            Id = updatedArticle.Id,
            Title = updatedArticle.Title,
            Content = updatedArticle.Content,
            CreatedAt = updatedArticle.CreatedAt,
            UpdatedAt = updatedArticle.UpdatedAt,
            CategoryId = updatedArticle.CategoryId,
            CategoryName = updatedArticle.Category?.Name ?? "Unknown Category",
            SubcategoryId = updatedArticle.SubcategoryId,
            SubcategoryName = updatedArticle.Subcategory?.Name ?? "Unknown Subcategory",
            TechnologyId = updatedArticle.TechnologyId,
            TechnologyName = updatedArticle.Technology?.Name ?? "Unknown Technology",
            UserId = updatedArticle.UserId,
            AuthorNickName = updatedArticle.User?.NickName ?? "Unknown Author",
            CommentCount = updatedArticle.Comments?.Count ?? 0
        };
    }

    public async Task DeleteArticleAsync(int id)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article == null)
            throw new KeyNotFoundException($"Article with ID {id} not found.");

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
    }
    
}
