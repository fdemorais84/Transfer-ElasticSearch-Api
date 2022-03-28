using AcessoTransfer.Data.Context;
using AcessoTransfer.Data.Repository;
using AcessoTransfer.Domain.Interfaces;
using AcessoTransfer.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcessoTransfer.Api
{
    public static class DependencyInjectionSetup
    {

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AcessoDbContext>();
            services.AddScoped<ITransferenciaRepository, TransferenciaRepository>();
            services.AddScoped<ITransferenciaService, TransferenciaService>();

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IUser, AspNetUser>();

            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }

    }
}
