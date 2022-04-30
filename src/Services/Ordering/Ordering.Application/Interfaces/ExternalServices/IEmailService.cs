using Ordering.Application.Models;
using System.Threading.Tasks;

namespace Ordering.Application.Interfaces.ExternalServices
{
    public interface IEmailService
    {
        Task<bool> SendEmail(SendEmailRequest emailRequest);
    }
}
