using GosteriZamani.API.Base;
using GosteriZamani.API.Models.Category;

namespace GosteriZamani.API.AbstractServices;

public interface ICategoryService
{
    Task<AppResult<List<CategoryResponse>>> GetAllAsync();
    Task<AppResult<CategoryResponse>> GetByIdAsync(string id);
    Task<AppResult<CategoryResponse>> CreateAsync(CreateCategoryDto createCategoryDto);
    Task<AppResult<CategoryResponse>> UpdateAsync(UpdateCategoryDto updateCategoryDto);
    Task<AppResult<NoContentDto>> DeleteAsync(string id);
}
