using GosteriZamani.API.AbstractServices;
using GosteriZamani.API.Base;
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

    public async Task<AppResult<CategoryResponse>> CreateAsync(CreateCategoryDto createCategoryDto)
    {
        var existCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Name == createCategoryDto.Name);
        if(existCategory is not null)
        {
            return AppResult<CategoryResponse>.Fail("Category already exists.",400);
        }
        var category = createCategoryDto.Adapt<Category>();
        await _context.Categories.AddAsync(category, cancellationToken: default);
        await _context.SaveChangesAsync();

        return AppResult<CategoryResponse>.Success(category.Adapt<CategoryResponse>(),201);
    }

    public async Task<AppResult<NoContentDto>> DeleteAsync(string id)
    {
        var category = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return AppResult<NoContentDto>.Success(204);
    }

    public async Task<AppResult<List<CategoryResponse>>> GetAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();

        return AppResult<List<CategoryResponse>>.Success(categories.Adapt<List<CategoryResponse>>());
    }

    public async Task<AppResult<CategoryResponse>> GetByIdAsync(string id)
    {
        var category = await _context.Categories.FindAsync(id);

        return AppResult<CategoryResponse>.Success(category.Adapt<CategoryResponse>());

    }

    public async Task<AppResult<CategoryResponse>> UpdateAsync(UpdateCategoryDto updateCategoryDto)
    {
        var existCategory= await _context.Categories.FindAsync(updateCategoryDto.Id);

        existCategory.Name = updateCategoryDto.Name;
        _context.Categories.Update(existCategory);
        await _context.SaveChangesAsync();

        return AppResult<CategoryResponse>.Success(existCategory.Adapt<CategoryResponse>(),200);
    }
}
