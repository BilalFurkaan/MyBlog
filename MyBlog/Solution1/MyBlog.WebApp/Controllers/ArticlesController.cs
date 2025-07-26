using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ArticleViewModel;
using WebApp.Models.CommentViewModel;
using WebApp.Services.ArticleApiService;
using WebApp.Services.CommentApiService;
using WebApp.Helpers;
using CommentViewModel = WebApp.Models.ArticleViewModel.CommentViewModel;


namespace WebApp.Controllers;

public class ArticlesController : Controller
{
    private readonly IArticleApiService _articleApiService;
    private readonly ICommentApiService _commentApiService;

    public ArticlesController(IArticleApiService articleApiService, ICommentApiService commentApiService)
    {
        _articleApiService = articleApiService;
        _commentApiService = commentApiService;
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
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
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
    
    [HttpPost]
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

    [HttpPost]
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
}