using KLMPNHomeStay.Models.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace KLMPNHomeStay.Services
{
    public interface IEmailService
    {
        public Task Send(string toEmail, string to_Name, string subject, string html);
    }
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(EmailConfiguration emailConfig)
        {

            _emailConfig = emailConfig;

        }

        public async Task Send(string toEmail, string to_Name, string subject, string html)
        {
            var senderEmail = new MailboxAddress(_emailConfig.Alias, _emailConfig.FromAddress);
            var receiverEmail = new MailboxAddress(to_Name, toEmail);

            // create message
            var email = new MimeMessage();
            email.From.Add(senderEmail);
            email.To.Add(receiverEmail);
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };
            
            // send email
            using (var client = new SmtpClient())
            {
                try
                {
                    // Disable_CertificateValidation();
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.CheckCertificateRevocation = false;
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync("crl", "ARQ)o5");
                    await client.SendAsync(email);
                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception, or both.
                    throw ex;
                }
            }
        }
        static void Disable_CertificateValidation()
        {
            // Disabling certificate validation because I can't find a way to make this work at the moment.
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
        }
        bool MyServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // Note: The following code casts to an X509Certificate2 because it's easier to get the
            // values for comparison, but it's possible to get them from an X509Certificate as well.
            if (certificate is X509Certificate2 certificate2)
            {
                var cn = certificate2.GetNameInfo(X509NameType.SimpleName, false);
                var fingerprint = certificate2.Thumbprint;
                var serial = certificate2.SerialNumber;
                var issuer = certificate2.Issuer;

                return cn == "imap.gmail.com" && issuer == "CN=GTS CA 1O1, O=Google Trust Services, C=US" &&
                    serial == "00A15434C2695FB1880300000000CBF786" &&
                    fingerprint == "F351BCB631771F19AF41DFF22EB0A0839092DA51";
            }

            return false;
        }
    }
}
