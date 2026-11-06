using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestfulCatalog.Application.DTOs.Category;
using RestfulCatalog.Application.Interfaces.Services;

namespace RestfulCatalog.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories =
                await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var category =
                await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            [FromBody] CreateCategoryDto dto)
        {
            var id =
                await _categoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                null);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateCategoryDto dto)
        {
            await _categoryService.UpdateAsync(
                id,
                dto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);

            return NoContent();
        }
    }
}
