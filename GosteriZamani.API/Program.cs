using FluentValidation;
using GosteriZamani.API.DbContexts;
using GosteriZamani.API.Filters;
using GosteriZamani.API.Models.Category;
using GosteriZamani.API.Services;
using GosteriZamani.API.Validations.Category;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// validator kayd�
builder.Services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryValidator>();

// filter kendisi DI'de olmal�
builder.Services.AddScoped<ValidationFilter>();

// AddControllers i�inde filter'� DI �zerinden ekle (AddService)
builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ValidationFilter>(); // <- �ok �nemli
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<GosteriZamaniDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        options =>
        {
            options.MigrationsAssembly(Assembly.GetAssembly(typeof(GosteriZamaniDbContext))!.GetName().Name);
        });
});



//Register services

builder.Services.AddScoped<ICategoryService, CategoryService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
