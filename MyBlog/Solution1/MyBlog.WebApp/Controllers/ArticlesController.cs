using Microsoft.AspNetCore.Mvc;
using WebApp.Services.ArticleApiService;
using WebApp.Models.ArticleViewModel;

namespace WebApp.Controllers;

public class ArticlesController : Controller
{
    private readonly IArticleApiService _articleApiService;


    public ArticlesController(IArticleApiService articleApiService)
    {
        _articleApiService = articleApiService;
    }
    
    public async Task<IActionResult> Index()
    {
        var articles = await _articleApiService.GetAllArticlesAsync();
        return View(articles);
    }

    public async Task<IActionResult> ArticleByCategory(int id)
    {
        var articles = await _articleApiService.GetArticlesByCategoryAsync(id);
        ViewBag.CategoryId = id;
        return View("Index", articles);
    }
    
    public async Task<IActionResult> ArticleBySubCategory(int id)
    {
        var articles = await _articleApiService.GetArticlesBySubCategoryAsync(id);
        ViewBag.SubCategoryId = id;
        return View("Index", articles);
    }

    public async Task<IActionResult> ArticleByTechnology(int id)
    {
        var articles = await _articleApiService.GetArticleByTechnologyAsync(id);
        ViewBag.TechnologyId = id;
        return View("Index", articles);
    }
    
    public async Task<IActionResult> Detail(int id)
    {
        var article = await _articleApiService.GetArticleDetailAsync(id);
        if (article == null)
        {
            return NotFound();
        }
        return View(article);
    }
    
}