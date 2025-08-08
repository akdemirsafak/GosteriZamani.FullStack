using GosteriZamani.API.DbContexts;
using GosteriZamani.API.Entities;
using GosteriZamani.API.Models.Category;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GosteriZamani.API.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly GosteriZamaniDbContext _context;

    public CategoryService(GosteriZamaniDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryResponse> CreateAsync(CreateCategoryDto createCategoryDto)
    {
        var category = createCategoryDto.Adapt<Category>();
        await _context.Categories.AddAsync(category, cancellationToken: default);
        await _context.SaveChangesAsync();
        return category.Adapt<CategoryResponse>();
    }

    public async Task DeleteAsync(string id)
    {
        var category = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CategoryResponse>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories.Adapt<List<CategoryResponse>>();
    }

    public async Task<CategoryResponse> GetByIdAsync(string id)
    {
        var category = await _context.Categories.FindAsync(id);
        return category.Adapt<CategoryResponse>();
    }

    public async Task<CategoryResponse> UpdateAsync(UpdateCategoryDto updateCategoryDto)
    {
        var existCategory= await _context.Categories.FindAsync(updateCategoryDto.Id);

        existCategory.Name = updateCategoryDto.Name;
        _context.Categories.Update(existCategory);
        await _context.SaveChangesAsync();
        return existCategory.Adapt<CategoryResponse>();
    }
}
