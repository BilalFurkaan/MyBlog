using WebApp.Models.ArticleViewModel;

namespace WebApp.Services.ArticleApiService;

public interface IArticleApiService
{
    Task<List<ArticleListViewModel>>GetAllArticlesAsync();
    Task<List<ArticleListViewModel>> GetArticlesByCategoryAsync(int categoryId);
    Task<List<ArticleListViewModel>> GetArticlesBySubCategoryAsync(int subCategoryId);
    Task<List<ArticleListViewModel>> GetArticleByTechnologyAsync(int technologyId);
    Task<ArticleDetailViewModel>GetArticleDetailAsync(int articleId);
}