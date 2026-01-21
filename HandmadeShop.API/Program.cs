using HandmadeShop.API.Middlewares;
using HandmadeShop.Application.Features.Inventory;
using HandmadeShop.Application.Features.Orders.Events;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Observers;
using HandmadeShop.Application.Services;
using HandmadeShop.Infrastructure.Persistence;
using HandmadeShop.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// SQL Connection
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HandmadeShopDBContext>(option => option.UseSqlServer(connection));
// UnitOfWork Service
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<EventDispatcher>();
builder.Services.AddScoped<IHandmadeObserver<OrderCreatedEvent>, InventoryHandler>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();