using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Dtos.SubcategoryDtos;
using MyBlog.Application.Usecasess.SubcategoryServices;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryService _subcategoryService;

        public SubcategoryController(ISubcategoryService subcategoryService)
        {
            _subcategoryService = subcategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultSubcategoryDto>>> GetAllSubcategories()
        {
            var subcategories = await _subcategoryService.GetAllSubcategoriesAsync();
            return Ok(subcategories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultSubcategoryDto>> GetSubcategoryById(int id)
        {
            try
            {
                var subcategory = await _subcategoryService.GetSubcategoryByIdAsync(id);
                return Ok(subcategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<DetailSubcategoryDto>> GetSubcategoryDetail(int id)
        {
            try
            {
                var subcategory = await _subcategoryService.GetSubcategoryDetailsAsync(id);
                return Ok(subcategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ResultSubcategoryDto>>> GetSubcategoriesByCategoryId(int categoryId)
        {
            var subcategories = await _subcategoryService.GetSubcategoriesByCategoryIdAsync(categoryId);
            return Ok(subcategories);
        }
        [HttpPost]
        public async Task<ActionResult<ResultSubcategoryDto>> CreateSubcategory(CreateSubcategoryDto createSubcategoryDto)
        {
            try
            {
                var subcategory = await _subcategoryService.CreateSubcategoryAsync(createSubcategoryDto);
                return CreatedAtAction(nameof(GetSubcategoryById), new { id = subcategory.Id }, subcategory);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResultSubcategoryDto>> UpdateSubcategory(UpdateSubcategoryDto updateSubcategoryDto)
        {
            try
            {
                var subcategory = await _subcategoryService.UpdateSubcategoryAsync(updateSubcategoryDto);
                return Ok(subcategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            try
            {
                await _subcategoryService.DeleteSubcategoryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
