using DevOpsPalermo.Models;
using DevOpsPalermo.Services.Repositories;
using DevOpsPalermo.Services;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Agregar SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddHealthChecks();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.MapHealthChecks("/health");


app.UseCors("CorsPolicy");
app.MapGet("/", () => " En vivo!");


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseHttpMetrics();
app.UseAuthorization();

app.MapControllers();
app.UseMetricServer();
app.UseHttpMetrics();
app.MapMetrics();

app.Run();
