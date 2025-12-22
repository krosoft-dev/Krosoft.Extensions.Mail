using Krosoft.Extensions.Mail.Abstractions.Interfaces;
using Krosoft.Extensions.Mail.Abstractions.Models;
using Krosoft.Extensions.Mail.Mailjet.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Mail.Mailjet.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMailMailjet(this IServiceCollection services,
                                                    IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
        services.AddTransient<IEmailService, MailjetEmailService>();

        return services;
    }
}