using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Exceptions;
using Ordering.Application.Interfaces.ExternalServices;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    /// <summary>
    /// Email service implementing <see href="https://docs.sendgrid.com/">SendGrid v3 API C#</see>.
    /// </summary>
    public class EmailService : IEmailService
    {
        public readonly EmailSettings _emailSettings;
        public readonly SendGridClient _emailClient;

        public readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = mailSettings.Value;
            _logger = logger;

            _emailClient = new SendGridClient(_emailSettings.ApiKey);
        }

        public async Task<bool> SendEmail(SendEmailRequest emailRequest)
        {
            var subject = emailRequest.Subject;
            var emailBody = emailRequest.Body;
            var to = new EmailAddress(emailRequest.To);
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await _emailClient.SendEmailAsync(sendGridMessage);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted && response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _logger.LogError("Email sending failed.");
                throw new EmailException($"Unexpected response status {response.StatusCode} when sending email.");
            }

            _logger.LogInformation($"Email sent to {emailRequest.To}.");
            return true;
        }
    }
}
