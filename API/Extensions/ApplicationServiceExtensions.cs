using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service){

            service.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            service.AddScoped<IProductRepository, ProductRepository>();
            service.Configure<ApiBehaviorOptions>(options =>{

                options.InvalidModelStateResponseFactory = actionContext =>{
                    var errors = actionContext.ModelState
                    .Where(e=>e.Value.Errors.Count > 0)
                    .SelectMany(x=>x.Value.Errors)
                    .Select(x=>x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return service;
        }
    }
}