using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_ipcom_pim_console.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(List<string> recipients, string subject, string body, bool isHtml = false)
        {
            try
            {
                // Get SMTP settings from configuration
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var smtpUsername = _configuration["EmailSettings:Username"];
                var smtpPassword = _configuration["EmailSettings:Password"];
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var senderName = _configuration["EmailSettings:SenderName"];
                var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]);

                // Log configuration values (without exposing password)
                _logger.LogInformation(
                    "Email configuration: Server={Server}, Port={Port}, Username={Username}, SenderEmail={SenderEmail}, EnableSsl={EnableSsl}",
                    smtpServer, smtpPort, smtpUsername, senderEmail, enableSsl);

                // Create mime message
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                
                // Add recipients
                foreach (var recipient in recipients)
                {
                    message.To.Add(new MailboxAddress("", recipient));
                }
                
                message.Subject = subject;

                // Set body
                var bodyBuilder = new BodyBuilder();
                if (isHtml)
                    bodyBuilder.HtmlBody = body;
                else
                    bodyBuilder.TextBody = body;
                
                message.Body = bodyBuilder.ToMessageBody();
                
                // Add diagnostic header for troubleshooting
                message.Headers.Add("X-IPCOM-Test", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

                _logger.LogInformation($"Sending email to {recipients.Count} recipients");
                
                // Send using MailKit
                using (var client = new SmtpClient())
                {
                    // For debugging/logging connection issues
                    client.Connected += OnSmtpClientConnected;
                    client.Authenticated += OnSmtpClientAuthenticated;
                    client.MessageSent += OnSmtpClientMessageSent;

                    // For detailed logging
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    
                    // Connect to server
                    await client.ConnectAsync(smtpServer, smtpPort, 
                        enableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
                    
                    // Authenticate
                    await client.AuthenticateAsync(smtpUsername, smtpPassword);
                    
                    // Send email
                    await client.SendAsync(message);
                    
                    // Disconnect
                    await client.DisconnectAsync(true);
                }
                
                _logger.LogInformation("Email sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email: {Message}", ex.Message);
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner exception: {Message}", ex.InnerException.Message);
                }
                throw;
            }
        }

        private void OnSmtpClientConnected(object sender, MailKit.ConnectedEventArgs e)
        {
            _logger.LogInformation($"Connected to SMTP server: {e.Host}");
        }

        private void OnSmtpClientAuthenticated(object sender, MailKit.AuthenticatedEventArgs e)
        {
            _logger.LogInformation("SMTP authentication successful");
        }

        private void OnSmtpClientMessageSent(object sender, MailKit.MessageSentEventArgs e)
        {
            _logger.LogInformation($"Message sent with ID: {e.Response}");
        }
    }
}