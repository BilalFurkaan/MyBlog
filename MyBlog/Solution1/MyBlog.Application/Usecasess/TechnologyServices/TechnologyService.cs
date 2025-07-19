using MyBlog.Application.Dtos.TechonologyDtos;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entites;

namespace MyBlog.Application.Usecasess.TechnologyServices;

public class TechnologyService: ITechnologyService
{
    private readonly IRepository<Technology> _repository;

    public TechnologyService(IRepository<Technology> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ResultTechnologyDto>> GetAllTechnologiesAsync()
    {
        var technologies = await _repository.GetAllWithIncludeAsync(
            t => t.Subcategory,
            t => t.Subcategory.Category
        );
        return technologies.Select(t => new ResultTechnologyDto()
        {
            Id = t.Id,
            Name = t.Name,
            SubcategoryId = t.SubcategoryId,
            SubcategoryName = t.Subcategory?.Name ?? "Unknown Subcategory",
            CategoryName = t.Subcategory?.Category?.Name ?? "Unknown Category"
        });
    }

    public async Task<ResultTechnologyDto> GetTechnologyByIdAsync(int id)
    {
        var technology = await _repository.GetByIdWithIncludeAsync(id, t => t.Subcategory, t => t.Subcategory.Category);
        if (technology == null)
        {
            throw new KeyNotFoundException($"Technology with ID {id} not found.");
        }
        return new ResultTechnologyDto
        {
            Id = technology.Id,
            Name = technology.Name,
            SubcategoryId = technology.SubcategoryId,
            SubcategoryName = technology.Subcategory?.Name ?? "Unknown Subcategory",
            CategoryName = technology.Subcategory?.Category?.Name ?? "Unknown Category"
        };
    }

    public async Task<ResultTechnologyDto> CreateTechnologyAsync(CreateTechnologyDto createTechnologyDto)
    {
        var technology = new Technology
        {
            Name = createTechnologyDto.Name,
            SubcategoryId = createTechnologyDto.SubcategoryId
        };

        await _repository.AddAsync(technology);
        await _repository.SaveChangesAsync();
        var createdTechnology = await _repository.GetByIdWithIncludeAsync(technology.Id, t => t.Subcategory, t => t.Subcategory.Category);
        return new ResultTechnologyDto
        {
            Id = createdTechnology.Id,
            Name = createdTechnology.Name,
            SubcategoryId = createdTechnology.SubcategoryId,
            SubcategoryName = createdTechnology.Subcategory?.Name ?? "Unknown Subcategory",
            CategoryName = createdTechnology.Subcategory?.Category?.Name ?? "Unknown Category"
        };
    }

    public async Task<ResultTechnologyDto> UpdateTechnologyAsync(UpdateTechnologyDto updateTechnologyDto)
    {
        var technology = await _repository.GetByIdAsync(updateTechnologyDto.Id);
        if (technology == null)
        {
            throw new KeyNotFoundException($"Technology with ID {updateTechnologyDto.Id} not found.");
        }
        technology.Name = updateTechnologyDto.Name;
        technology.SubcategoryId = updateTechnologyDto.SubcategoryId;
        await _repository.UpdateAsync(technology);
        await _repository.SaveChangesAsync();
        var updatedTechnology = await _repository.GetByIdWithIncludeAsync(technology.Id, t => t.Subcategory, t => t.Subcategory.Category);
        return new ResultTechnologyDto
        {
            Id = updatedTechnology.Id,
            Name = updatedTechnology.Name,
            SubcategoryId = updatedTechnology.SubcategoryId,
            SubcategoryName = updatedTechnology.Subcategory?.Name ?? "Unknown Subcategory",
            CategoryName = updatedTechnology.Subcategory?.Category?.Name ?? "Unknown Category"
        };
    }

    public async Task DeleteTechnologyAsync(int id)
    {
        var technology = await _repository.GetByIdAsync(id);
        if (technology == null)
        {
            throw new KeyNotFoundException($"Technology with ID {id} not found.");
        }
        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
    }

    public async Task<DetailTechnologyDto> GetDetailTechnologyAsync(int id)
    {
        var technology = await _repository.GetByIdWithIncludeAsync(id, t => t.Subcategory, t => t.Subcategory.Category, t => t.Articles);
        if (technology == null)
        {
            throw new KeyNotFoundException($"Technology with ID {id} not found.");
        }
        return new DetailTechnologyDto
        {
            Id = technology.Id,
            Name = technology.Name,
            SubcategoryId = technology.SubcategoryId,
            SubcategoryName = technology.Subcategory?.Name ?? "Unknown Subcategory",
            CategoryName = technology.Subcategory?.Category?.Name ?? "Unknown Category",
            ArticleCount = technology.Articles?.Count ?? 0
        };
    }

    public async Task<IEnumerable<ResultTechnologyDto>> GetTechnologiesBySubcategoryIdAsync(int subcategoryId)
    {
        var technologies = await _repository.GetAllWithIncludeAsync(
            t => t.Subcategory,
            t => t.Subcategory.Category
        );
        var filteredTechnologies = technologies.Where(t => t.SubcategoryId == subcategoryId);
        return filteredTechnologies.Select(t => new ResultTechnologyDto
        {
            Id = t.Id,
            Name = t.Name,
            SubcategoryId = t.SubcategoryId,
            SubcategoryName = t.Subcategory?.Name ?? "Unknown Subcategory",
            CategoryName = t.Subcategory?.Category?.Name ?? "Unknown Category"
        });
    }
}