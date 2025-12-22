namespace Krosoft.Extensions.Mail.Abstractions.Interfaces;

public interface IEmailService
{
    Task SendAsync(string to,
                   string subject,
                   string html,
                   CancellationToken cancellationToken);
}