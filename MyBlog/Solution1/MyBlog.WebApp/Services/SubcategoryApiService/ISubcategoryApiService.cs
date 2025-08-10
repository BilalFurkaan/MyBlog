using WebApp.Models.SubcategoryViewModel;

namespace WebApp.Services.SubcategoryApiService;

public interface ISubcategoryApiService
{
    Task<List<SubcategoryListViewModel>> GetSubcategoriesByCategory(int categoryId);
}