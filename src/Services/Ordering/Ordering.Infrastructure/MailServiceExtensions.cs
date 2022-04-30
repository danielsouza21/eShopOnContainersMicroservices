using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Interfaces.ExternalServices;
using Ordering.Application.Models;
using Ordering.Infrastructure.Mail;

namespace Ordering.Infrastructure
{
    public static class MailServiceExtensions
    {
        public static IServiceCollection AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailSettings>(c => configuration.GetSection(InfrastructureConstants.EMAIL_SETTINGS_CONFIG_KEY));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
