using Catalog.API.Middlewares;
using Catalog.Infrastructure.Repository;
using Catalog.Infrastructure.Repository.Context;
using Catalog.Infrastructure.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Catalog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICatalogContext, CatalogContext>();
            services.AddSingleton<IProductRepository, ProductRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(AppConstants.SWAGGER_APP_VERSION, new OpenApiInfo { 
                    Title = AppConstants.SWAGGER_APP_NAME, 
                    Version = AppConstants.SWAGGER_APP_VERSION 
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(
                    AppConstants.SWAGGER_URL, 
                    string.Format(
                        AppConstants.SWAGGER_NAME_FORMAT, 
                        AppConstants.SWAGGER_APP_NAME,
                        AppConstants.SWAGGER_APP_VERSION)
                    )
                );
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
