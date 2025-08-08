using GosteriZamani.API.Models.Category;

namespace GosteriZamani.API.Services;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetAllAsync();
    Task<CategoryResponse> GetByIdAsync(string id);
    Task<CategoryResponse> CreateAsync(CreateCategoryDto createCategoryDto);
    Task<CategoryResponse> UpdateAsync(UpdateCategoryDto updateCategoryDto);
    Task DeleteAsync(string id);
}
