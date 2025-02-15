using API.Middleware;
using Core.Interfaces;
using Infrastracture.configuration;
using Infrastracture.Data;
using Infrastracture.Repositories;
using Infrastracture.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt=>{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped( typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddCors();
// redis regesteration
builder.Services.AddSingleton<IConnectionMultiplexer>(config => {
    var configStr = builder.Configuration.GetConnectionString("Redis");
    if (configStr == null) throw new Exception("Cannot get redis connection string");
    var configuration = ConfigurationOptions.Parse(configStr, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSingleton<ICartService, CartService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    try
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider;
        var context = service.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context);
    }
    catch (Exception e)
    {
        Console.WriteLine($"An error occurred while applying migrations: {e.Message}");

    }
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));

app.MapControllers();
//try
//{
//    using var scope = app.Services.CreateScope();
//    var service = scope.ServiceProvider;
//    var context = service.GetRequiredService<StoreContext>();
//    await context.Database.MigrateAsync();
//    await StoreContextSeed.SeedAsync(context);
//}
//catch (Exception e)
//{
//    Console.WriteLine(e);
//    throw;

//}

app.Run();
