

using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();


//middleware for handling errors
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
//to connect with frontEnd 
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

//place of this function declaration is matters 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
 }
 catch(Exception ex)
 {
    var logger =  services.GetService<ILogger<Program>>();
    logger.LogError(ex,"An error occured during migrations");
 }


app.Run();
