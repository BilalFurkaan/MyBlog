using Microsoft.AspNetCore.Http;
using MyBlog.Application.Dtos.ArticleDtos;

namespace MyBlog.Application.Usecasess.ArticleServices;

public interface IArticleService
{
    Task<IEnumerable<ResultArticleDto>> GetAllArticlesAsync();
    Task<ResultArticleDto> GetArticleByIdAsync(int id);
    Task<DetailArticleDto> GetArticleDetailAsync(int id);
    Task<IEnumerable<ResultArticleDto>> GetArticlesByCategoryIdAsync(int categoryId);
    Task<IEnumerable<ResultArticleDto>> GetArticlesBySubcategoryIdAsync(int subcategoryId);
    Task<IEnumerable<ResultArticleDto>> GetArticlesByTechnologyIdAsync(int technologyId);
    Task<IEnumerable<ResultArticleDto>> GetArticlesByUserIdAsync(string userId);
    Task<PagedResult<ResultArticleDto>> GetPagedArticlesAsync(int page, int pageSize);
    Task<ResultArticleDto> CreateArticleAsync(CreateArticleDto dto);
    Task<ResultArticleDto> UpdateArticleAsync(UpdateArticleDto dto);
    Task DeleteArticleAsync(int id);

}