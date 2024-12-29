using Car_Management.Data;
using Car_Management.Data.Repositories;
using Car_Management.Data.Repositories.Interfaces;
using Car_Management.Services;
using Car_Management.Services.Interfaces;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Car_Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new DateOnlyJSONConverter());
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default Connection")));

            

            builder.Services.AddTransient<IGarageRepository, GarageRepository>();
            builder.Services.AddTransient<IGarageService, GarageService>();
            

            builder.Services.AddTransient<ICarRepository, CarRepository>();
            builder.Services.AddTransient<ICarService, CarService>();


            builder.Services.AddTransient<IMaintenanceRepository, MaintenanceRepository>();
            builder.Services.AddTransient<IMaintenanceService, MaintenanceService>();

            builder.Services.AddCors();


            /* builder.Services.AddCors(options =>
             {
                 options.AddPolicy("AllowNginx", policy =>
                 {
                     policy.WithOrigins("http://localhost:8088")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                 });
             });
 */


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //app.UseCors("AllowNginx");

            app.UseCors(builder =>
             builder.WithOrigins("http://localhost:3000") // React app URL
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapControllers();

            app.Run();
        }

    }
}
