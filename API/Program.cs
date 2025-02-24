using API.Extensions;
using API.Middleware;
using Core.Entities;
using Infrastracture.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAppServices();
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<StoreContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<StoreContext>();
    try
    {
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

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();


app.Run();
