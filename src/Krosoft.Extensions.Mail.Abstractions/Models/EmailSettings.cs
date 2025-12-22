namespace Krosoft.Extensions.Mail.Abstractions.Models;

public class EmailSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? From { get; set; }
    public IEnumerable<string>? RecipientsDebug { get; set; }
}