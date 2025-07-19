using MyBlog.Application.Dtos.CategoryDtos;
using MyBlog.Application.Interfaces;
using MyBlog.Domain.Entites;

namespace MyBlog.Application.Usecasess.CategoryServices;

public class CategoryService: ICategoryService
{
    private readonly IRepository<Category> _repository;

    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ResultCategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(c => new ResultCategoryDto
        {
            Id = c.Id,
            Name = c.Name,
        });
    }

    public async Task<ResultCategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {id} not found.");
        }

        return new ResultCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
        };
    }

    public async Task<DetailCategoryDto> GetCategoryDetailAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {id} not found.");
        }
        var allcategories = await _repository.GetAllAsync();
        var subcategoryCount = allcategories.Where(c => c.Id == id)
            .SelectMany(c => c.Subcategories).Count();

        var articleCount = allcategories.Where(c => c.Id == id)
            .SelectMany(c => c.Articles).Count();
        return new DetailCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            SubcategoryCount = subcategoryCount,
            ArticleCount = articleCount
        };
    }

    public async Task<ResultCategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        if (string.IsNullOrWhiteSpace(createCategoryDto.Name))
        {
            throw new ArgumentException("Category name cannot be empty.");
        }
        var category = new Category
        {
            Name = createCategoryDto.Name
        };
        
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync(); 
        
        return new ResultCategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<ResultCategoryDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        var existingCategory = await _repository.GetByIdAsync(updateCategoryDto.Id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {updateCategoryDto.Id} not found.");
        }
        existingCategory.Name = updateCategoryDto.Name;
        await _repository.UpdateAsync(existingCategory);
        await _repository.SaveChangesAsync();
        return new ResultCategoryDto
        {
            Id = existingCategory.Id,
            Name = existingCategory.Name
        };

    }

    public async Task DeleteCategoryAsync(int id)
    {
       
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {id} not found.");
        }
        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
    }
}