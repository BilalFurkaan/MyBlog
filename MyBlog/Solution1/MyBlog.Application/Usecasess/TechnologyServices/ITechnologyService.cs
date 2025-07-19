using MyBlog.Application.Dtos.TechonologyDtos;

namespace MyBlog.Application.Usecasess.TechnologyServices;

public interface ITechnologyService
{
    Task <IEnumerable<ResultTechnologyDto>> GetAllTechnologiesAsync();
    Task<ResultTechnologyDto> GetTechnologyByIdAsync(int id);
    Task<ResultTechnologyDto> CreateTechnologyAsync(CreateTechnologyDto createTechnologyDto);
    Task<ResultTechnologyDto> UpdateTechnologyAsync(UpdateTechnologyDto updateTechnologyDto);
    Task DeleteTechnologyAsync(int id);
    Task<DetailTechnologyDto> GetDetailTechnologyAsync(int id);
    Task<IEnumerable<ResultTechnologyDto>> GetTechnologiesBySubcategoryIdAsync(int subcategoryId);
    
}