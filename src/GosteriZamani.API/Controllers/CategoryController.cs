using GosteriZamani.API.Base;
using GosteriZamani.API.Models.Category;
using GosteriZamani.API.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace GosteriZamani.API.Controllers;


public class CategoryController : CustomBaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(AppResult<List<CategoryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var response = await _categoryService.GetAllAsync();
        return CreateActionResult(response);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AppResult<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(string id)
    {
      return CreateActionResult(await _categoryService.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
    {
        return CreateActionResult(await _categoryService.CreateAsync(createCategoryDto));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateCategoryDto updateCategoryDto)
    {
        return CreateActionResult(await _categoryService.UpdateAsync(updateCategoryDto));
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(AppResult<NoContentDto>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(string id)
    {        
        return CreateActionResult(await _categoryService.DeleteAsync(id));
    }
}