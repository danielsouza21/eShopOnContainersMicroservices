using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ordering.Application.Behaviours;
using System.Reflection;

namespace Ordering.API.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDependenciesInjectionAndServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.ConfigureCqrsMediatrWithValidation();

            return services;
        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(AppConstants.SWAGGER_APP_VERSION, new OpenApiInfo
                {
                    Title = AppConstants.SWAGGER_APP_NAME,
                    Version = AppConstants.SWAGGER_APP_VERSION
                });
            });
        }

        private static void ConfigureCqrsMediatrWithValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
