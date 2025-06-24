
using Microsoft.EntityFrameworkCore;
using Testing1.Business.Abstract;
using Testing1.Business.Concrete;
using Testing1.Context;
using Testing1.Middlewares;

namespace Testing1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var config = builder.Configuration;
            builder.Services.AddDbContext<ProductContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ApiConnection"));
            });

            builder.Services.AddScoped<IProductService, ProductService>();
            var app = builder.Build();
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
