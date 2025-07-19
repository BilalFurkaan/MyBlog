using MyBlog.Application.Dtos.SubcategoryDtos;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entites;

namespace MyBlog.Application.Usecasess.SubcategoryServices;

public class SubcategoryService: ISubcategoryService
{
    private readonly IRepository<Subcategory> _repository;

    public SubcategoryService(IRepository<Subcategory> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ResultSubcategoryDto>> GetAllSubcategoriesAsync()
    {
        var subcategories = await _repository.GetAllWithIncludeAsync(s => s.Category);
        return subcategories.Select(s => new ResultSubcategoryDto
        {
            Id = s.Id,
            Name = s.Name,
            CategoryId = s.CategoryId,
            CategoryName = s.Category?.Name ?? "Unknown Category"
        });
    }

    public async Task<ResultSubcategoryDto> GetSubcategoryByIdAsync(int id)
    {
        var subcategory = await _repository.GetByIdWithIncludeAsync(id, s => s.Category);
        if (subcategory == null)
        {
            throw new KeyNotFoundException($"Subcategory with ID {id} not found.");
        }
        return new ResultSubcategoryDto
        {
            Id = subcategory.Id,
            Name = subcategory.Name,
            CategoryId = subcategory.CategoryId,
            CategoryName = subcategory.Category?.Name ?? "Unknown Category"
        };
    }

    public async Task<DetailSubcategoryDto> GetSubcategoryDetailsAsync(int id)
    {
        var subcategory = await _repository.GetByIdWithIncludeAsync(id, s => s.Category, s => s.Technologies, s => s.Articles);
        
        if (subcategory == null)
        {
            throw new KeyNotFoundException($"Subcategory with ID {id} not found.");
        }

        return new DetailSubcategoryDto
        {
            Id = subcategory.Id,
            Name = subcategory.Name,
            CategoryId = subcategory.CategoryId,
            CategoryName = subcategory.Category?.Name ?? "Unknown Category",
            TechnologyCount = subcategory.Technologies?.Count ?? 0,
            ArticleCount = subcategory.Articles?.Count ?? 0
        };
    }

    public async Task<IEnumerable<ResultSubcategoryDto>> GetSubcategoriesByCategoryIdAsync(int categoryId)
    {
        var subcategories = await _repository.GetAllWithIncludeAsync(s => s.Category);
        var filteredSubcategories = subcategories.Where(s => s.CategoryId == categoryId);
        return filteredSubcategories.Select(s => new ResultSubcategoryDto
        {
            Id = s.Id,
            Name = s.Name,
            CategoryId = s.CategoryId,
            CategoryName = s.Category?.Name ?? "Unknown Category"
        });
    }

    public async Task<ResultSubcategoryDto> CreateSubcategoryAsync(CreateSubcategoryDto createSubcategoryDto)
    {
        if (string.IsNullOrWhiteSpace(createSubcategoryDto.Name))
        { 
            throw new ArgumentException("Bu alan adı boş olamaz.", nameof(createSubcategoryDto.Name));
        }
        var subcategory = new Subcategory
        {
            Name = createSubcategoryDto.Name,
            CategoryId = createSubcategoryDto.CategoryId
        };
        await _repository.AddAsync(subcategory);
        await _repository.SaveChangesAsync();
        
        // CategoryName'i almak için yeniden çekiyoruz
        var createdSubcategory = await _repository.GetByIdWithIncludeAsync(subcategory.Id, s => s.Category);
        return new ResultSubcategoryDto
        {
            Id = createdSubcategory.Id,
            Name = createdSubcategory.Name,
            CategoryId = createdSubcategory.CategoryId,
            CategoryName = createdSubcategory.Category?.Name ?? "Unknown Category"
        };
    }

    public async Task<ResultSubcategoryDto> UpdateSubcategoryAsync(UpdateSubcategoryDto updateSubcategoryDto)
    {
        if (string.IsNullOrWhiteSpace(updateSubcategoryDto.Name))
        {
            throw new ArgumentException("Bu alan adı boş olamaz.", nameof(updateSubcategoryDto.Name));
        }
        var subcategory = await _repository.GetByIdAsync(updateSubcategoryDto.Id);
        if (subcategory == null)
        {
            throw new KeyNotFoundException($"Subcategory with ID {updateSubcategoryDto.Id} not found.");
        }
        
        subcategory.Name = updateSubcategoryDto.Name;
        subcategory.CategoryId = updateSubcategoryDto.CategoryId;
        await _repository.UpdateAsync(subcategory);
        await _repository.SaveChangesAsync();
        
        // CategoryName'i almak için yeniden çekiyoruz
        var updatedSubcategory = await _repository.GetByIdWithIncludeAsync(subcategory.Id, s => s.Category);
        return new ResultSubcategoryDto
        {
            Id = updatedSubcategory.Id,
            Name = updatedSubcategory.Name,
            CategoryId = updatedSubcategory.CategoryId,
            CategoryName = updatedSubcategory.Category?.Name ?? "Unknown Category"
        };
    }

    public async Task DeleteSubcategoryAsync(int id)
    {
        var subcategory = await _repository.GetByIdAsync(id);
        if (subcategory == null)
        {
            throw new KeyNotFoundException($"Girmiş olduğunuz alan id si:  {id} bulunamadı.");
        }
        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
    }
}