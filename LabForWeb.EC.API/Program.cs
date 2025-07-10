
using LabForWeb.EC.DAL;
using LabForWeb.EC.API.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LabForWeb.EC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine(Assembly.GetExecutingAssembly());
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Add services to the container.
            #region Database
            builder.Services.AddDbContext<ECContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(builder.Configuration.GetConnectionString("default"), o => o.MigrationsAssembly("LabForWeb.EC.API"))
            );
            
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // aggiunge documentazione swaggere
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            #region Applicazione Migrazioni
            app.MigrateDatabase();
            #endregion

            app.Run();
        }
    }
}
