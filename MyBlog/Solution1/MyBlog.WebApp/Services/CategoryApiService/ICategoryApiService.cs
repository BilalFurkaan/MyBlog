using MyBlog.Domain.Entites;
using WebApp.Models.CategoryViewModel;

namespace WebApp.Services.CategoryApiService;

public interface ICategoryApiService
{
    Task<List<CategoryListViewModel>> GetAllCategories();
}