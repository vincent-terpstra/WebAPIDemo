using Application.Abstractions;
using DataAccess;
using MinimalAPIDemo;
using WebAPIDemo.Extensions;
using WebAPIDemo.Filters;

namespace WebAPIDemo;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(
            cfg =>
            {
                cfg.Filters.Add(typeof(ExceptionHandlerFilter));
            });
        
        
        builder.RegisterServices();
        
        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            var scope = app.Services.CreateScope();
            var users = scope.ServiceProvider.GetRequiredService<IUserService>();
            users.PopulateDbUsers();

            var posts = scope.ServiceProvider.GetRequiredService<IPostRepository>();
            posts.PopulateDbPosts();

        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        
        app.RegisterExceptionHandling();
        app.MapControllers();
        app.RegisterEndpoints();

        app.Run();
    }
}