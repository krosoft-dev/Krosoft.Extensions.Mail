using Krosoft.Extensions.Mail.Abstractions.Models;

namespace Krosoft.Extensions.Mail.Abstractions.Extensions;

public static class EmailSettingsExtension
{
    public static IEnumerable<string> GetRecipients(this EmailSettings emailSettings, string to)
    {
        if (emailSettings.RecipientsDebug != null && emailSettings.RecipientsDebug.Any())
        {
            return emailSettings.RecipientsDebug;
        }

        return new List<string> { to };
    }

    public static IEnumerable<T> GetRecipients<T>(this EmailSettings emailSettings,
                                                  string to,
                                                  Func<string, T> action)
    {
        var recipients = emailSettings.GetRecipients(to);
        var items = new List<T>();
        foreach (var recipient in recipients)
        {
            items.Add(action(recipient));
        }

        return items;
    }
}