using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.APIS.Extentions
{
    public static class ApplicationsServiceExtentions
    {


        public static void AddApplicationServicies(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddSingleton(typeof(IResponseCacheService), typeof(ResponseCacheService));
            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.AddSingleton(typeof(IBasketRepository), typeof(BasketRepository));
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            Services.Configure<ApiBehaviorOptions>(options =>
            {

                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                             .SelectMany(e => e.Value.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };

            });
        }


        public static WebApplication UseSwaggerMiddleWare(this WebApplication app)
        {

            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
