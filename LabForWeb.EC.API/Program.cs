
using LabForWeb.EC.DAL;
using LabForWeb.EC.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LabForWeb.EC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Database
            builder.Services.AddDbContext<ECContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(builder.Configuration.GetConnectionString("default"))
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

            app.UseAuthorization();


            app.MapControllers();

            #region Applicazione Migrazioni
            app.MigrateDatabase();
            #endregion

            app.Run();
        }
    }
}
