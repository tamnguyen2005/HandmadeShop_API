using HandmadeShop.Application.DTOs.Category;
using HandmadeShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory(CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _categoryService.AddCategoryAsync(request);
            return StatusCode(StatusCodes.Status201Created, new { message = "Create new category succeccfully !" });
        }
    }
}