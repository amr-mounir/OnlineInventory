using Firebase.Database;
using Microsoft.EntityFrameworkCore;
using OnlineInventory.API.Shared;
using OnlineInventory.Application.Interfaces;
using OnlineInventory.Application.Services;
using OnlineInventory.Infrastructure.Context;
using OnlineInventory.Infrastructure.Repositories;

namespace OnlineInventory.API
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

            builder.Services.AddDbContext<OnlineInventoryDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStrings")));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.Configure<FirebaseConfig>(builder.Configuration.GetSection("FirebaseConfig"));
            builder.Services.Configure<FirebaseConfig>(builder.Configuration.GetSection("FirebaseConfig"));
            builder.Services.AddSingleton(provider =>
            {
                var firebaseConfig = provider.GetRequiredService<IConfiguration>().Get<FirebaseConfig>();
                return new FirebaseClient(firebaseConfig.DatabaseUrl);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}