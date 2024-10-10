﻿using Azure;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.APIS.Extentions
{
    public static class ApplicationsServiceExtentions
    {
        
        
        public static void AddApplicationServicies(this IServiceCollection Services) 
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.Configure<ApiBehaviorOptions>(options => {

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
