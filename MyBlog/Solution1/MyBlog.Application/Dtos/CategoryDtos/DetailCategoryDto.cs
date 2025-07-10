namespace MyBlog.Application.Dtos.CategoryDtos;

public class DetailCategoryDto:ResultCategoryDto
{
    public int SubcategoryCount { get; set; }  // Alt kategori sayısı
    public int ArticleCount { get; set; }   // Makale sayısı
}