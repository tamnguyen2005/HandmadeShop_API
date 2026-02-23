using HandmadeShop.API.Middlewares;
using HandmadeShop.API.Services;
using HandmadeShop.Application.Events.Orders;
using HandmadeShop.Application.Events.Reviews;
using HandmadeShop.Application.Features.Inventory;
using HandmadeShop.Application.Handlers.Rating;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Decorators;
using HandmadeShop.Application.Patterns.Observers;
using HandmadeShop.Application.Patterns.Singleton;
using HandmadeShop.Application.Services;
using HandmadeShop.Infrastructure.Persistence;
using HandmadeShop.Infrastructure.Provider;
using HandmadeShop.Infrastructure.Repository;
using HandmadeShop.Infrastructure.Security;
using HandmadeShop.Infrastructure.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//************** Mới đổi tên file đó ***********
var builder = WebApplication.CreateBuilder(args);
// SQL Connection
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HandmadeShopDBContext>(option => option.UseSqlServer(connection));
// UnitOfWork Service
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<EventDispatcher>();
builder.Services.AddScoped<IHandmadeObserver<OrderCreatedEvent>, InventoryHandler>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductService>(provider =>
{
    var innerService = provider.GetRequiredService<ProductService>();
    var cache = provider.GetRequiredService<IMemoryCache>();
    return new CacheProductService(innerService, cache);
});
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IHandmadeObserver<ReviewCreatedEvent>, RatingHandler>();
builder.Services.AddSingleton<UserConnectionManager>();
builder.Services.AddSignalR();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
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
app.MapHub<NotificationHub>("/notificationHub");
app.MapControllers();

app.Run();