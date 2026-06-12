using Microsoft.EntityFrameworkCore;
using UESAN.SHOPPING.CORE.Core.Interfaces;
using UESAN.SHOPPING.CORE.Core.Services;
using UESAN.SHOPPING.CORE.Infrastructure.Data;
using UESAN.SHOPPING.CORE.Infrastructure.Repositories;
using UESAN.SHOPPING.CORE.Infrastructure.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _config = builder.Configuration;
var cnx = _config.GetConnectionString("DevConnection");
//SQL Server
builder.Services.AddDbContext<StoreDbContext>(options =>
  options.UseSqlServer(cnx));
// PostreSQL
//builder.Services.AddDbContext<StoreDbContext>(options =>
  //options.UseNpgsql(cnx));

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddSharedInfrastructure(_config);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        //WithOrigins("http://localhost:3000") // Reemplaza con el origen de tu frontend
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
