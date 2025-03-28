using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dotnet_ipcom_pim_console.Services;

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

                // Create mail message
                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, senderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml
                };

                // Add recipients
                foreach (var recipient in recipients)
                {
                    mailMessage.To.Add(recipient);
                }

                // Create SMTP client
                using var smtpClient = new SmtpClient
                {
                    Host = smtpServer,
                    Port = smtpPort,
                    EnableSsl = enableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    Timeout = 30000 // 30 seconds timeout
                };
                

                _logger.LogInformation($"Sending email to {recipients.Count} recipients");
                
                // Send email
                await smtpClient.SendMailAsync(mailMessage);
                
                _logger.LogInformation("Email sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email");
                throw;
            }
        }
    }