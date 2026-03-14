using HandmadeShop.API.Middlewares;
using HandmadeShop.API.Services;
using HandmadeShop.Application.Events.Orders;
using HandmadeShop.Application.Features.Inventory;
using HandmadeShop.Application.Features.Notification;
using HandmadeShop.Application.Interfaces;
using HandmadeShop.Application.Patterns.Decorators;
using HandmadeShop.Application.Patterns.Observers;
using HandmadeShop.Application.Patterns.Singleton;
using HandmadeShop.Application.Services;
using HandmadeShop.Infrastructure.Cache;
using HandmadeShop.Infrastructure.Persistence;
using HandmadeShop.Infrastructure.Provider;
using HandmadeShop.Infrastructure.Repository;
using HandmadeShop.Infrastructure.Security;
using HandmadeShop.Infrastructure.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// SQL Connection
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
var redis = builder.Configuration.GetConnectionString("RedisConnection");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redis;
});
builder.Services.AddDbContext<HandmadeShopDBContext>(option => option.UseSqlServer(connection));
// UnitOfWork Service
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<EventDispatcher>();
builder.Services.AddScoped<IHandmadeObserver<OrderCreatedEvent>, InventoryHandler>();
builder.Services.AddScoped<IHandmadeObserver<OrderCreatedEvent>, NotificationHandler>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ICacheService, RedisService>();
builder.Services.AddScoped<IProductService>(provider =>
{
    var innerService = provider.GetRequiredService<ProductService>();
    var cache = provider.GetRequiredService<ICacheService>();
    return new CacheProductService(innerService, cache);
});
builder.Services.AddScoped<INotificationService, NotificationService>();
//builder.Services.AddScoped<IHandmadeObserver<ReviewCreatedEvent>, RatingHandler>();
builder.Services.AddSingleton<UserConnectionManager>();
builder.Services.AddSignalR();
builder.Services.AddScoped<ICartService, CartService>();
//builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICouponService, CouponService>();
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
    options.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                    path.Value!.Contains("hub", StringComparison.OrdinalIgnoreCase))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(origin => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var app = builder.Build();
app.UseSwaggerUI(c =>
{
    c.ConfigObject.AdditionalItems["persistAuthorization"] = false;
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
//{"protocol":"json","version":1}
app.MapHub<NotificationHub>("/notificationHub");
app.MapHub<ChatHub>("/chatHub");
app.MapControllers();

app.Run();