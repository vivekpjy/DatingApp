

using API.Extensions;
using API.Middleware;

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

app.Run();
