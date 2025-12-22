using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Core.Tools;
using Krosoft.Extensions.Mail.Abstractions.Extensions;
using Krosoft.Extensions.Mail.Abstractions.Interfaces;
using Krosoft.Extensions.Mail.Abstractions.Models;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Krosoft.Extensions.Mail.Mailjet.Services;

public class MailjetEmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<MailjetEmailService> _logger;

    public MailjetEmailService(IOptions<EmailSettings> appSettings, ILogger<MailjetEmailService> logger)
    {
        _logger = logger;
        _emailSettings = appSettings.Value;
    }

    public async Task SendAsync(string to, string subject, string html, CancellationToken cancellationToken)
    {
        Guard.IsNotNullOrWhiteSpace(nameof(to), to);
        Guard.IsNotNullOrWhiteSpace(nameof(html), html);

        var recipients = _emailSettings.GetRecipients(to, s => new SendContact(s));

        var client = new MailjetClient(_emailSettings.Username,
                                       _emailSettings.Password);

        var email = new TransactionalEmailBuilder()
                    .WithFrom(new SendContact(_emailSettings.From))
                    .WithSubject(subject)
                    .WithHtmlPart(html)
                    .WithTo(recipients)
                    .Build();

        var response = await client.SendTransactionalEmailAsync(email);

        foreach (var message in response.Messages)
        {
            if (message.Errors != null && message.Errors.Any())
            {
                throw new KrosoftTechnicalException(message.Errors.Select(e => e.ErrorMessage.ToString()).ToHashSet());
            }

            if (message.To != null && message.To.Any())
            {
                _logger.LogInformation($"Mail envoyé (statut : '{message.Status}') aux destinataires : {string.Join(", ", message.To.Select(t => t.Email))}");
            }
        }
    }
}