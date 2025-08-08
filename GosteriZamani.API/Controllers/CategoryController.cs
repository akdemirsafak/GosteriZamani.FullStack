using GosteriZamani.API.Models.Category;
using GosteriZamani.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GosteriZamani.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _categoryService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
      return Ok(await _categoryService.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
    {

        return Ok(await _categoryService.CreateAsync(createCategoryDto));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateCategoryDto updateCategoryDto)
    {
        return Ok(await _categoryService.UpdateAsync(updateCategoryDto));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }
}