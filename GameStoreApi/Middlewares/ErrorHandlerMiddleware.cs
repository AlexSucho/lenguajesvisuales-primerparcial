using System.Net;
using System.Text.Json;


namespace GameStoreApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next) { _next = next; }


        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                ctx.Response.ContentType = "application/json";
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var payload = new { message = "Ocurrió un error inesperado", detail = ex.Message };
                await ctx.Response.WriteAsync(JsonSerializer.Serialize(payload));
            }
        }
    }
}