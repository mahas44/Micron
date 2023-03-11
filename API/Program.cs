using ElasticCore;
using API;
using API.Infrastructure;
using API.Middlewares;
using Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient(typeof(IElasticCoreService<>), typeof(ElasticCoreService<>));
builder.Services.AddSingleton<ElasticClientProvider>();
//builder.Services.AddScoped<LogFilter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("CoreSwagger", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Swagger on ASP.NET Core",
        Version = "v1",
        Description = "Micron Web API"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //c.DefaultModelsExpandDepth(-1);
        c.SwaggerEndpoint("/swagger/CoreSwagger/swagger.json", "Swagger Test .Net Core");
    });
}
app.UseMiddleware<RequestResponseMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
