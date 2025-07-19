using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Dtos.TechonologyDtos;
using MyBlog.Application.Usecasess.TechnologyServices;

namespace MyBlog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechnologyController : ControllerBase
    {
        private readonly ITechnologyService _technologyService;

        public TechnologyController(ITechnologyService technologyService)
        {
            _technologyService = technologyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultTechnologyDto>>> GetAllTechnologies()
        {
            var technologies = await _technologyService.GetAllTechnologiesAsync();
            return Ok(technologies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultTechnologyDto>> GetTechnologyById(int id)
        {
            try
            {
                var technology = await _technologyService.GetTechnologyByIdAsync(id);
                return Ok(technology);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<DetailTechnologyDto>> GetDetailTechnology(int id)
        {
            try
            {
                var technology = await _technologyService.GetDetailTechnologyAsync(id);
                return Ok(technology);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("subcategory/{subcategoryId}")]
        public async Task<ActionResult<IEnumerable<ResultTechnologyDto>>> GetTechnologiesBySubcategoryId(int subcategoryId)
        {
            var technologies = await _technologyService.GetTechnologiesBySubcategoryIdAsync(subcategoryId);
            return Ok(technologies);
        }

        [HttpPost]
        public async Task<ActionResult<ResultTechnologyDto>> CreateTechnology(CreateTechnologyDto createTechnologyDto)
        {
            try
            {
                var technology = await _technologyService.CreateTechnologyAsync(createTechnologyDto);
                return CreatedAtAction(nameof(GetTechnologyById), new { id = technology.Id }, technology);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResultTechnologyDto>> UpdateTechnology(UpdateTechnologyDto updateTechnologyDto)
        {
            try
            {
                var technology = await _technologyService.UpdateTechnologyAsync(updateTechnologyDto);
                return Ok(technology);
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
        public async Task<IActionResult> DeleteTechnology(int id)
        {
            try
            {
                await _technologyService.DeleteTechnologyAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
