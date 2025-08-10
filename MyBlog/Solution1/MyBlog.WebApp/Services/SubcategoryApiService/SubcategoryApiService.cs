using WebApp.Models.SubcategoryViewModel;

namespace WebApp.Services.SubcategoryApiService;

public class SubcategoryApiService: ISubcategoryApiService
{
    private readonly HttpClient _httpClient;

    public SubcategoryApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SubcategoryListViewModel>> GetSubcategoriesByCategory(int categoryId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/Subcategory/category/{categoryId}");
            
            if (response.IsSuccessStatusCode)
            {
                var subcategories = await response.Content.ReadFromJsonAsync<List<SubcategoryListViewModel>>();
                return subcategories ?? new List<SubcategoryListViewModel>();
            }
            
            // Log error response
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Error: {response.StatusCode} - {errorContent}");
            
            return new List<SubcategoryListViewModel>();
        }
        catch (HttpRequestException ex)
        {
            // Network error
            Console.WriteLine($"Network Error: {ex.Message}");
            return new List<SubcategoryListViewModel>();
        }
        catch (Exception ex)
        {
            // Unexpected error
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            return new List<SubcategoryListViewModel>();
        }
    }
}