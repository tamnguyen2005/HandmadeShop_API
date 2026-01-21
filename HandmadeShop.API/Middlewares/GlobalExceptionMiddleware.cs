using System.Net;
using System.Text.Json;

namespace HandmadeShop.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = new { message = ex.Message };
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case InvalidOperationException e: context.Response.StatusCode = (int)HttpStatusCode.Conflict; break;
                case KeyNotFoundException e: context.Response.StatusCode = (int)HttpStatusCode.NotFound; break;
                case ArgumentException e: context.Response.StatusCode = (int)HttpStatusCode.BadRequest; break;
            }
            var result = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(result);
        }
    }
}