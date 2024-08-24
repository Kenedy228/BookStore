using System.Text.Json;
using WebApplication1.Context;
using WebApplication1.Infrastructure;
using WebApplication1.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddTransient<IBookService, BookService>();
            builder.Services.AddDbContext<BookContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            app.MapControllers();

            app.UseCors(builder =>
            {
                builder.WithHeaders().AllowAnyHeader().AllowAnyMethod();
                builder.WithOrigins("http://localhost:3000");
            });

            app.Run();
        }
    }
}