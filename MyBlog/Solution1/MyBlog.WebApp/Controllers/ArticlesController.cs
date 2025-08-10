using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models.ArticleViewModel;
using WebApp.Models.CommentViewModel;
using WebApp.Services.ArticleApiService;
using WebApp.Services.CommentApiService;
using WebApp.Helpers;
using WebApp.Services.CategoryApiService;
using WebApp.Services.SubcategoryApiService;
using WebApp.Services.TechnologiesApiService;
using CommentViewModel = WebApp.Models.ArticleViewModel.CommentViewModel;


namespace WebApp.Controllers;
[Route("[controller]")]
public class ArticlesController : Controller
{
    private readonly IArticleApiService _articleApiService;
    private readonly ICommentApiService _commentApiService;
    private readonly ICategoryApiService _categoryApiService;
    private readonly ISubcategoryApiService _subcategoryApiService;
    private readonly ITechnologiesApiService _technologiesApiService;

    public ArticlesController(IArticleApiService articleApiService, ICommentApiService commentApiService, ICategoryApiService categoryApiService, ISubcategoryApiService subcategoryApiService, ITechnologiesApiService technologiesApiService)
    {
        _articleApiService = articleApiService;
        _commentApiService = commentApiService;
        _categoryApiService = categoryApiService;
        _subcategoryApiService = subcategoryApiService;
        _technologiesApiService = technologiesApiService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var articles = await _articleApiService.GetAllArticlesAsync();
        return View(articles);
    }

    [HttpGet("category/{id}")]
    public async Task<IActionResult> ArticleByCategory(int id)
    {
        var articles = await _articleApiService.GetArticlesByCategoryAsync(id);
        ViewBag.CategoryId = id;
        return View("Index", articles);
    }
    
    [HttpGet("subcategory/{id}")]
    public async Task<IActionResult> ArticleBySubCategory(int id)
    {
        var articles = await _articleApiService.GetArticlesBySubCategoryAsync(id);
        ViewBag.SubCategoryId = id;
        return View("Index", articles);
    }

    [HttpGet("technology/{id}")]
    public async Task<IActionResult> ArticleByTechnology(int id)
    {
        var articles = await _articleApiService.GetArticleByTechnologyAsync(id);
        ViewBag.TechnologyId = id;
        return View("Index", articles);
    }
    
    [HttpGet("detail/{id}")]
    public async Task<IActionResult> Detail(int id)
    {
        var article = await _articleApiService.GetArticleDetailAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        // Yorumları al
        var comments = await _commentApiService.GetCommentsByArticleAsync(id);
        // Kullanıcı kimliğini JWT'den al
        var token = Request.Cookies["access_token"];
        string currentUserId = null;
        if (!string.IsNullOrEmpty(token))
        {
            currentUserId = JwtHelper.GetClaimFromToken(token, "nameid");
        }

        // ResultCommentViewModel'i CommentViewModel'e dönüştür
        var commentViewModels = new List<CommentViewModel>();
        if (comments != null)
        {
            foreach (var comment in comments)
            {
                commentViewModels.Add(new CommentViewModel
                {
                    Id = comment.Id,
                    UserId = comment.UserId,
                    Content = comment.Content,
                    CommenterNickName = comment.CommenterNickName,
                    CreatedAt = comment.CreatedAt,
                    IsMine = comment.UserId == currentUserId
                });
            }
        }

        // Makale modeline yorumları ekle
        article.Comments = commentViewModels;

        return View(article);
    }
    [HttpGet("create")]
    public async Task <IActionResult> Create()
    {
        var model = new CreateArticleViewModel();
        var categories = await _categoryApiService.GetAllCategories();
        model.Categories = categories.Select(c=> new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();
        return View(model);
    }

    [HttpGet("subcategories/{categoryId}")]
    public async Task<IActionResult> GetSubcategoriesByCategory(int categoryId)
    {
        try
        {
            var subcategories = await _subcategoryApiService.GetSubcategoriesByCategory(categoryId);
            return Json(subcategories);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = "Alt kategoriler yüklenirken hata oluştu" });
        }
    }

    [HttpGet("technologies/{subcategoryId}")]
    public async Task<IActionResult> GetTechnologiesBySubcategory(int subcategoryId)
    {
        try
        {
            var technologies = await _technologiesApiService.GetTechnologiesInSubcategory(subcategoryId);
            return Json(technologies);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = "Teknolojiler yüklenirken hata oluştu" });
        }
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateArticleViewModel createArticleViewModel)
    {
        var token = Request.Cookies["access_token"];
        string userId = null;
        if (!string.IsNullOrEmpty(token))
        {
            userId = JwtHelper.GetClaimFromToken(token, "nameid");
        }
        if (string.IsNullOrEmpty(userId))
        {
            ModelState.AddModelError("", "Kullanıcı kimliği bulunamadı. Lütfen giriş yapın.");
            return Unauthorized();
        }
        createArticleViewModel.UserId = userId;
        ModelState.Remove(nameof(createArticleViewModel.UserId));

        if (!ModelState.IsValid)
        {
            ViewBag.ValidationErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return View(createArticleViewModel);
        }

        var result = await _articleApiService.CreateArticleAsync(createArticleViewModel);
        if (result)
        {
            return RedirectToAction("Index");
        }
        ModelState.AddModelError("", "Makale oluşturulamadı. Lütfen tekrar deneyin.");
        return View(createArticleViewModel);
    }
    
    [HttpPost("add-comment")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(CreateCommentViewModel model)
    {
        // Kullanıcı kimliğini JWT'den al
        var token = Request.Cookies["access_token"];
        string userId = null;
        if (!string.IsNullOrEmpty(token))
        {
            userId = JwtHelper.GetClaimFromToken(token, "nameid");
        }
        if (string.IsNullOrEmpty(userId))
        {
            TempData["CommentError"] = "Yorum eklemek için giriş yapmalısınız.";
            return RedirectToAction("Detail", new { id = model.ArticleId });
        }

        model.UserId = userId;
        ModelState.Remove(nameof(model.UserId)); // UserId'yi ModelState'den kaldır

        if (!ModelState.IsValid)
        {
            var validationErrors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            var validationMessage = string.Join(", ", validationErrors);
            TempData["CommentError"] = $"Validation hatası: {validationMessage}";
            return RedirectToAction("Detail", new { id = model.ArticleId });
        }

        // API servisine gönder
        var (success, errorMessage) = await _commentApiService.AddCommentAsync(model);
        if (success)
        {
            TempData["CommentSuccess"] = "Yorumunuz başarıyla eklendi!";
        }
        else
        {
            TempData["CommentError"] = errorMessage ?? "Yorum eklenemedi. Lütfen tekrar deneyin.";
        }

        return RedirectToAction("Detail", new { id = model.ArticleId });
    }

    [HttpPost("delete-comment")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        // Kullanıcı kimliğini JWT'den al
        var token = Request.Cookies["access_token"];
        string userId = null;
        if (!string.IsNullOrEmpty(token))
        {
            userId = JwtHelper.GetClaimFromToken(token, "nameid");
        }
        if (string.IsNullOrEmpty(userId))
        {
            TempData["CommentError"] = "Yorum silmek için giriş yapmalısınız.";
            return RedirectToAction("Index");
        }

        // API servisine gönder
        var (success, errorMessage) = await _commentApiService.DeleteCommentAsync(commentId);
        if (success)
        {
            TempData["CommentSuccess"] = "Yorum başarıyla silindi!";
        }
        else
        {
            TempData["CommentError"] = errorMessage ?? "Yorum silinemedi. Lütfen tekrar deneyin.";
        }

        // Makale detayına geri dön (commentId'den articleId'yi alamadığımız için Index'e yönlendir)
        return RedirectToAction("Index");
    }
    [HttpGet("my-articles")]
    public async Task<IActionResult> MyArticles()
    {
        // Kullanıcı kimliğini JWT'den al
        var token = Request.Cookies["access_token"];
        string userId = null;
        if (!string.IsNullOrEmpty(token))
        {
            userId = JwtHelper.GetClaimFromToken(token, "nameid");
        }
        if (string.IsNullOrEmpty(userId))
        {
            ModelState.AddModelError("", "Kullanıcı kimliği bulunamadı. Lütfen giriş yapın.");
            return Unauthorized();
        }

        var articles = await _articleApiService.GetUserArticle(userId);
        return View(articles);
    }
    
    [HttpPost("delete-article")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteArticle(int articleId)
    {
        var token = Request.Cookies["access_token"];        
        if (string.IsNullOrEmpty(token))
        {
            TempData["ErrorMessage"] = "Silme işlemi için giriş yapmalısınız.";
            return RedirectToAction("MyArticles");
        }

        var success = await _articleApiService.DeleteArticleAsync(articleId);
        if (success)
        {
            TempData["SuccessMessage"] = "Makale başarıyla silindi.";
        }
        else
        {
            TempData["ErrorMessage"] = "Makale silinemedi. Lütfen tekrar deneyin.";
        }
        return RedirectToAction("MyArticles");
    }
    }
