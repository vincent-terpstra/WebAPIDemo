using Application.Abstractions;
using DataAccess;
using WebAPIDemo.Extensions;
using WebAPIDemo.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.RegisterExceptionHandling();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<KeyNotFoundMiddleware>();

app.MapControllers();

app.RegisterEndpoints();

app.Run();

public partial class Program {}