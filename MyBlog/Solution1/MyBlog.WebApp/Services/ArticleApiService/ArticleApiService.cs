using WebApp.Models.ArticleViewModel;


namespace WebApp.Services.ArticleApiService;

public class ArticleApiService: IArticleApiService
{
    private readonly HttpClient _httpClient;
    public ArticleApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ArticleListViewModel>> GetAllArticlesAsync()
    {
        var response = await _httpClient.GetAsync("/api/Article");
        response.EnsureSuccessStatusCode();
        var articles = await response.Content.ReadFromJsonAsync<List<ArticleListViewModel>>();
        return articles ?? new List<ArticleListViewModel>();
    }

    public async Task<List<ArticleListViewModel>> GetArticlesByCategoryAsync(int categoryId)
    {
        var response = await _httpClient.GetAsync($"/api/Article/category/{categoryId}");
        response.EnsureSuccessStatusCode();
        var articles = await response.Content.ReadFromJsonAsync<List<ArticleListViewModel>>();
        return articles ?? new List<ArticleListViewModel>();
    }

    public async Task<List<ArticleListViewModel>> GetArticlesBySubCategoryAsync(int subCategoryId)
    {
        var response =await _httpClient.GetAsync($"/api/Article/subcategory/{subCategoryId}");
        response.EnsureSuccessStatusCode();
        var articles = await response.Content.ReadFromJsonAsync<List<ArticleListViewModel>>();
        return articles ?? new List<ArticleListViewModel>();
    }

    public async Task<List<ArticleListViewModel>> GetArticleByTechnologyAsync(int technologyId)
    {
        var response = await _httpClient.GetAsync($"/api/Article/technology/{technologyId}");
        response.EnsureSuccessStatusCode();
        var articles = await response.Content.ReadFromJsonAsync<List<ArticleListViewModel>>();
        return articles ?? new List<ArticleListViewModel>();
    }

    public async Task<ArticleDetailViewModel> GetArticleDetailAsync(int articleId)
    {
        var response = await _httpClient.GetAsync($"/api/Article/detail/{articleId}");
        response.EnsureSuccessStatusCode();
        var article = await response.Content.ReadFromJsonAsync<ArticleDetailViewModel>();
        return article!;
    }
    
    public async Task<bool> CreateArticleAsync(CreateArticleViewModel createArticleViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Article", createArticleViewModel);
        return response.IsSuccessStatusCode;
    }
}