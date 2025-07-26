using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Dtos.ArticleDtos;
using MyBlog.Application.Usecasess.ArticleServices;

namespace MyBlog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultArticleDto>>> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return Ok(articles);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<ResultArticleDto>>> GetPagedArticles([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var paged = await _articleService.GetPagedArticlesAsync(page, pageSize);
            return Ok(paged);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultArticleDto>> GetArticleById(int id)
        {
            try
            {
                var article = await _articleService.GetArticleByIdAsync(id);
                return Ok(article);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<DetailArticleDto>> GetArticleDetail(int id)
        {
            try
            {
                var article = await _articleService.GetArticleDetailAsync(id);
                return Ok(article);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ResultArticleDto>>> GetArticlesByCategoryId(int categoryId)
        {
            var articles = await _articleService.GetArticlesByCategoryIdAsync(categoryId);
            return Ok(articles);
        }

        [HttpGet("subcategory/{subcategoryId}")]
        public async Task<ActionResult<IEnumerable<ResultArticleDto>>> GetArticlesBySubcategoryId(int subcategoryId)
        {
            var articles = await _articleService.GetArticlesBySubcategoryIdAsync(subcategoryId);
            return Ok(articles);
        }

        [HttpGet("technology/{technologyId}")]
        public async Task<ActionResult<IEnumerable<ResultArticleDto>>> GetArticlesByTechnologyId(int technologyId)
        {
            var articles = await _articleService.GetArticlesByTechnologyIdAsync(technologyId);
            return Ok(articles);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ResultArticleDto>>> GetArticlesByUserId(string userId)
        {
            var articles = await _articleService.GetArticlesByUserIdAsync(userId);
            return Ok(articles);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResultArticleDto>> CreateArticle(CreateArticleDto createArticleDto)
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (createArticleDto == null)
            {
                return BadRequest("Invalid article data.");
            }

            try
            {
                createArticleDto.UserId = userId;
                var article = await _articleService.CreateArticleAsync(createArticleDto);
                return CreatedAtAction(nameof(GetArticleById), new { id = article.Id }, article);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ResultArticleDto>> UpdateArticle(UpdateArticleDto updateArticleDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                // Makalenin sahibi mi kontrol et
                var article = await _articleService.GetArticleByIdAsync(updateArticleDto.Id);
                if (article == null)
                    return NotFound("Makale bulunamadı.");
                if (article.UserId != userId)
                    return Forbid("Bu makaleyi güncellemeye yetkiniz yok.");

                updateArticleDto.UserId = userId;
                var updatedArticle = await _articleService.UpdateArticleAsync(updateArticleDto);
                return Ok(updatedArticle);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                // Makalenin sahibi mi kontrol et
                var article = await _articleService.GetArticleByIdAsync(id);
                if (article == null)
                    return NotFound("Makale bulunamadı.");
                if (article.UserId != userId)
                    return Forbid("Bu makaleyi silmeye yetkiniz yok.");

                await _articleService.DeleteArticleAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
