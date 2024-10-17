using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIS.Extentions;
using Talabat.APIS.MiddleWare;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Identity;

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
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            //builder.Services.AddScoped<IGenericRepository<Product>,GenericRepository<Product>>();
            //builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            //builder.Services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((serverProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddApplicationServicies();
            builder.Services.AddIdentity<AppUser, IdentityRole>(options => 
            {
            
            
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();
            var _IdentitydbContext = services.GetRequiredService<AppIdentityDbContext>();
            var _UserManger = services.GetRequiredService<UserManager<AppUser>>();

            var logerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
                await _IdentitydbContext.Database.MigrateAsync();
                await AppIdentityDbContextSeed.SeedUserAsync(_UserManger);
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
