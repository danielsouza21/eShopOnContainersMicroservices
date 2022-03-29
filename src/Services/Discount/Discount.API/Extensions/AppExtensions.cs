using Microsoft.AspNetCore.Builder;

namespace Discount.API.Extensions
{
    public static class AppExtensions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
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
    }
}
