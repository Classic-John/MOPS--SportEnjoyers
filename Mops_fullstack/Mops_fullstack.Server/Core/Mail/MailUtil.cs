using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Mops_fullstack.Server.Datalayer.Models;
using MimeKit;

namespace Mops_fullstack.Server.Core.Mail
{
    public class MailUtil: IMailUtil
    {
        readonly AppSettings _appSettings;
        readonly SmtpClient _client;

        public MailUtil(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _client = new SmtpClient();
            _client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            _client.Authenticate(_appSettings.Email, _appSettings.Password);
        }

        public void SendVerificationMail(Player player)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(_appSettings.Name, _appSettings.Email));
            message.To.Add(new MailboxAddress(player.Name, player.Email));
            message.Subject = "SportsEnjoyers verification e-mail";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<p>Hi {player.Name}, click this <a href=\"https://localhost:63635/verify?code={player.VerificationCode}\">link</a> to verify your account!</p>";
            bodyBuilder.TextBody = $"Hi {player.Name}, click this link to verify your account: \"https://localhost:63635/verify?code={player.VerificationCode}\".";
            message.Body = bodyBuilder.ToMessageBody();

            _client.Send(message);
        }
    }
}
