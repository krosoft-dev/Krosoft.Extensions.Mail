using JetBrains.Annotations;
using Krosoft.Extensions.Mail.Abstractions.Extensions;
using Krosoft.Extensions.Mail.Abstractions.Models;

namespace Krosoft.Extensions.Mail.Abstractions.Tests.Extensions;

[TestClass]
[TestSubject(typeof(EmailSettingsExtension))]
public class EmailSettingsExtensionTest
{
    [TestMethod]
    public void GetRecipients_Should_Return_To_When_No_Debug_Recipients()
    {
        var settings = new EmailSettings();
        var result = settings.GetRecipients("test@mail.com").ToList();

        Check.That(result).ContainsExactly("test@mail.com");
    }

    [TestMethod]
    public void GetRecipients_Should_Return_Debug_Recipients_When_Present()
    {
        var settings = new EmailSettings
        {
            RecipientsDebug = new[] { "debug1@mail.com", "debug2@mail.com" }
        };

        var result = settings.GetRecipients("test@mail.com").ToList();

        Check.That(result).ContainsExactly("debug1@mail.com", "debug2@mail.com");
    }

    [TestMethod]
    public void GetRecipients_Generic_Should_Apply_Action_On_Recipients()
    {
        var settings = new EmailSettings
        {
            RecipientsDebug = new[] { "a@mail.com", "b@mail.com" }
        };

        var result = settings.GetRecipients("test@mail.com", s => s.ToUpper()).ToList();

        Check.That(result).ContainsExactly("A@MAIL.COM", "B@MAIL.COM");
    }
}