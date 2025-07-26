using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Dtos.CommentDtos;
using MyBlog.Application.Usecasess.CommentServices;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(id);
                return Ok(comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {
            var UserId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                createCommentDto.UserId = UserId;
                var comment = await _commentService.CreateCommentAsync(createCommentDto);
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment(int id, UpdateCommentDto updateCommentDto)
        {
            if (id != updateCommentDto.Id)
                return BadRequest("Id mismatch.");
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                // Yorumun sahibi mi kontrol et
                var comment = await _commentService.GetCommentByIdAsync(id);
                if (comment == null)
                    return NotFound("Yorum bulunamadı.");
                if (comment.UserId != UserId)
                    return Forbid("Bu yorumu güncellemeye yetkiniz yok.");

                updateCommentDto.UserId = UserId;
                var updatedComment = await _commentService.UpdateCommentAsync(updateCommentDto);
                return Ok(updatedComment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                // Yorumun sahibi mi kontrol et
                var comment = await _commentService.GetCommentByIdAsync(id);
                if (comment == null)
                    return NotFound("Yorum bulunamadı.");
                if (comment.UserId != UserId)
                    return Forbid("Bu yorumu silmeye yetkiniz yok.");
                
                await _commentService.DeleteCommentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("article/{articleId}")]
        public async Task<IActionResult> GetCommentsByArticleId(int articleId)
        {
            var comments = await _commentService.GetCommentsByArticleIdAsync(articleId);
            return Ok(comments);
        }
    }
}
