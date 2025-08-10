using WebApp.Models.TechnologiesViewModel;

namespace WebApp.Services.TechnologiesApiService;

public class TechnologiesApiService: ITechnologiesApiService
{
    private readonly HttpClient _httpClient;

    public TechnologiesApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TechnologiesListViewModel>> GetTechnologiesInSubcategory(int subcategoryId)
    {
        var response = await _httpClient.GetAsync($"api/Technology/subcategory/{subcategoryId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<TechnologiesListViewModel>>();
        }
        else
        {
            throw new Exception($"Failed to load technologies for subcategory {subcategoryId}");
        }
        
    }
}