using MyBlog.Application.Dtos.CategoryDtos;

namespace MyBlog.Application.Usecasess.CategoryServices;

public interface ICategoryService
{
    Task<IEnumerable<ResultCategoryDto>> GetAllCategoriesAsync();
    Task<ResultCategoryDto> GetCategoryByIdAsync(int id);
    Task<DetailCategoryDto> GetCategoryDetailAsync(int id);
    Task<ResultCategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<ResultCategoryDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
    Task DeleteCategoryAsync(int id);
}