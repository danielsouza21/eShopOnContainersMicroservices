using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ordering.Application.Behaviours;
using Ordering.Application.Mappings;
using Ordering.Infrastructure;
using System;
using System.Reflection;

namespace Ordering.API.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDependenciesInjectionAndServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
            services.AddSingleton(mapper);

            services.ConfigureCqrsMediatrWithValidation();
            services.ConfigureInfrastructureServices(configuration);

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

        private static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMailServices(configuration);
            services.AddPersistenceServices(configuration);            
            services.ConfigureMassTransitRabbitMq(configuration);
        }

        private static void ConfigureCqrsMediatrWithValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
