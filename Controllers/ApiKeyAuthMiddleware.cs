namespace Simple_User_Management_API.Controllers
{
    namespace UserApi.Middleware
    {
        public class ApiKeyAuthMiddleware
        {
            private readonly RequestDelegate _next;
            private const string APIKEY_HEADER = "X-API-KEY";
            private const string EXPECTED_KEY = "secret-sample-key"; // replace before publishing

            public ApiKeyAuthMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                // only protect write endpoints (simple example)
                if (context.Request.Method == HttpMethods.Post ||
                    context.Request.Method == HttpMethods.Put ||
                    context.Request.Method == HttpMethods.Delete)
                {
                    if (!context.Request.Headers.TryGetValue(APIKEY_HEADER, out var key) || key != EXPECTED_KEY)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsJsonAsync(new { message = "Unauthorized - invalid API key" });
                        return;
                    }
                }

                await _next(context);
            }
        }
    }

}
