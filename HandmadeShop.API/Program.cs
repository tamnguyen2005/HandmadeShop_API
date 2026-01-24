using HandmadeShop.API.Middlewares;
using HandmadeShop.Application.Features.Inventory;
using HandmadeShop.Application.Features.Orders.Events;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Observers;
using HandmadeShop.Application.Services;
using HandmadeShop.Infrastructure.Persistence;
using HandmadeShop.Infrastructure.Provider;
using HandmadeShop.Infrastructure.Repository;
using HandmadeShop.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Cấu hình cho jwt
var jwtSetting = builder.Configuration.GetSection("Jwt");
var key = jwtSetting["Secret"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSetting["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSetting["Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ClockSkew = TimeSpan.Zero
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();