using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Interfaces.ExternalServices;
using Ordering.Application.Models;

namespace Ordering.Infrastructure.Mail
{
    public static class MailServiceExtensions
    {
        private const string EMAIL_SETTINGS_CONFIG_KEY = "EmailSettings";

        public static IServiceCollection AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailSettings>(c => configuration.GetSection(EMAIL_SETTINGS_CONFIG_KEY));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
