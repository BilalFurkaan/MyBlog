using WebApp.Models.TechnologiesViewModel;

namespace WebApp.Services.TechnologiesApiService;

public interface ITechnologiesApiService
{
    Task<List<TechnologiesListViewModel>>GetTechnologiesInSubcategory(int subcategoryId);
}