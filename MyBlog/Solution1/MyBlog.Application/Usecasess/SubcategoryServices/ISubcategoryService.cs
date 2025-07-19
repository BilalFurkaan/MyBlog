using MyBlog.Application.Dtos.SubcategoryDtos;

namespace MyBlog.Application.Usecasess.SubcategoryServices;

public interface ISubcategoryService
{
    Task<IEnumerable<ResultSubcategoryDto>> GetAllSubcategoriesAsync();
    Task<ResultSubcategoryDto> GetSubcategoryByIdAsync(int id);
    Task<DetailSubcategoryDto> GetSubcategoryDetailsAsync(int id);
    Task<IEnumerable<ResultSubcategoryDto>> GetSubcategoriesByCategoryIdAsync(int categoryId);
    Task<ResultSubcategoryDto> CreateSubcategoryAsync(CreateSubcategoryDto createSubcategoryDto);
    Task<ResultSubcategoryDto> UpdateSubcategoryAsync(UpdateSubcategoryDto updateSubcategoryDto);
    Task DeleteSubcategoryAsync(int id);
}