using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// In-memory "database" as singleton
builder.Services.AddSingleton<List<Simple_User_Management_API.Models.UserApi.Models.User>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom middleware
app.UseMiddleware<Simple_User_Management_API.Controllers.UserApi.Middleware.RequestLoggingMiddleware>();
app.UseMiddleware<Simple_User_Management_API.Controllers.UserApi.Middleware.ApiKeyAuthMiddleware>(); // simple api-key auth for admin endpoints

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
