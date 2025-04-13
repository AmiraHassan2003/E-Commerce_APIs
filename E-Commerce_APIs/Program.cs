using E_Commerce_APIs.Models.Context;
using E_Commerce_APIs.Repositories.CarRentalRepo;
using E_Commerce_APIs.Repositories.CategoryRepo;
using E_Commerce_APIs.Repositories.DetailsRepo;
using E_Commerce_APIs.Repositories.ProductRepo;
using E_Commerce_APIs.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<E_CommerceDbContext>(
    op => op.UseSqlServer(builder.Configuration.GetConnectionString("myConn"))
    );
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDetailsRepository, DetailsRepository>();
builder.Services.AddScoped<ICarRentalRepository, CarRentalRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();






builder.Services.AddAuthentication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
