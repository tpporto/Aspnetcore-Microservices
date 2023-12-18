using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger, [FromServices] EmailSettings EmailSettings)
        {
            _emailSettings = emailSettings.Value;
            if (string.IsNullOrWhiteSpace(_emailSettings.Host)) {
                _emailSettings = EmailSettings;
            }
            _logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            //var client = new SendGridClient(_emailSettings.ApiKey);
            try
            {
                var subject = email.Subject;
                var emailBody = email.Body;

                var from = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromName);

                MailMessage mail = new MailMessage()
                {
                    From = from,

                };
                mail.Subject = subject;

                mail.To.Add(new MailAddress(email.To));
                mail.Body = emailBody.ToString();
                mail.IsBodyHtml = true;


                using (SmtpClient smtp = new SmtpClient(_emailSettings.Host, (int)_emailSettings.Port))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false; // IMPORTANTE PARA LINUX
                    smtp.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
                    smtp.Host = _emailSettings.Host;
                    smtp.Port = _emailSettings.Port;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    // ATENCAO!! REMOVI O AWAIT AQUI PRA VER SE O USUARIO N É PUNIDO PELO TEMPO DE DISPARO DOS EMAILS. A IDEIA É QUE O PROGRAMA SIGA PROCESSANDO E O ENVIO DE EMAIL SEJA FEITO NO SEU TEPO, SEM SEGURAR O USUARIO NO FRONT// PORTO
                    await smtp.SendMailAsync(mail);
                    _logger.LogInformation("Email sent.");

                    return true;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("Email sending failed.");
                return false;
            }




        }
    }
}