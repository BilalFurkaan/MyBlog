using WebApp.Models.CategoryViewModel;

namespace WebApp.Services.CategoryApiService;

public class CategoryApiService: ICategoryApiService
{
    private readonly HttpClient _client;

    public CategoryApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<CategoryListViewModel>> GetAllCategories()
    {
        var response = await _client.GetFromJsonAsync<List<CategoryListViewModel>>("api/Category");
        if (response == null)
        {
            return new List<CategoryListViewModel>();
        }
        return response;
    }
}