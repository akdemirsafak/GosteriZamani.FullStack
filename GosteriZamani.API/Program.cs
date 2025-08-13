using FluentValidation;
using GosteriZamani.API.AbstractServices;
using GosteriZamani.API.DbContexts;
using GosteriZamani.API.Filters;
using GosteriZamani.API.Interceptors;
using GosteriZamani.API.Middlewares;
using GosteriZamani.API.Models.Category;
using GosteriZamani.API.Services;
using GosteriZamani.API.Validations.Category;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Swagger servisleri
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// validator kaydý
builder.Services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryValidator>();

// filter kendisi DI'de olmalý
builder.Services.AddScoped<ValidationFilter>();

// AddControllers içinde filter'ý DI üzerinden ekle (AddService)
builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ValidationFilter>(); // <- çok önemli
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddSingleton<AuditInterceptor>();

builder.Services.AddDbContext<GosteriZamaniDbContext>((sp,options) =>
{
    var interceptor = sp.GetService<AuditInterceptor>()!;
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        options =>
        {
            options.MigrationsAssembly(Assembly.GetAssembly(typeof(GosteriZamaniDbContext))!.GetName().Name);
        }).AddInterceptors(interceptor);
});

//Register services

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();


// Swagger UI’yi sadece Development ortamýnda açmak
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // /swagger açýldýðýnda direk UI gelsin
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Hazýrladýðýmýz global exception middleware
app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
