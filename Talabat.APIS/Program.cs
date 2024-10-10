
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIS.Errors;
using Talabat.APIS.Extentions;
using Talabat.APIS.Helpers;
using Talabat.APIS.MiddleWare;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped<IGenericRepository<Product>,GenericRepository<Product>>();
            //builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            //builder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();
            builder.Services.AddApplicationServicies();

            var app = builder.Build();

               using var scope = app.Services.CreateScope();
               var services = scope.ServiceProvider;
               var _dbContext = services.GetRequiredService<StoreContext>();

               var logerFactory= services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
            }
            catch (Exception ex)
            {
                var logger = logerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error occured during migration");
            }

            //var scope = app.Services.CreateScope();
            //try 
            //{
            //    var services = scope.ServiceProvider;
            //    var _dbContext = services.GetRequiredService<StoreContext>();
            //    await _dbContext.Database.MigrateAsync();
            //}
            //finally 
            //{
            //    scope.Dispose();
            //}




            // Configure the HTTP request pipeline.

            #region MiddleWares
            app.UseMiddleware<ExceptionMiddleWare>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }
            

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
