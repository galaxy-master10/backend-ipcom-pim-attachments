﻿using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dotnet_ipcom_pim_console.Services
{
    public class AttachmentExpiryNotificationService : IAttachmentExpiryNotificationService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<AttachmentExpiryNotificationService> _logger;
        private readonly IConfiguration _configuration;

        public AttachmentExpiryNotificationService(
            IAttachmentRepository attachmentRepository,
            IEmailService emailService,
            ILogger<AttachmentExpiryNotificationService> logger,
            IConfiguration configuration)
        {
            _attachmentRepository = attachmentRepository;
            _emailService = emailService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendExpiryNotificationsAsync()
        {
            _logger.LogInformation("Fetching attachments with upcoming expiration dates");

            // Get current date
            var today = DateTime.Today;

            // Get thresholds for different warning levels
            var fourWeeksFromNow = today.AddDays(28);
            var threeWeeksFromNow = today.AddDays(21);
            var twoWeeksFromNow = today.AddDays(14);
            var oneWeekFromNow = today.AddDays(7);

            // Get all attachments expiring within the next 4 weeks
            var attachments = await _attachmentRepository.GetAttachmentsForConsoleAppAsync();

            // Group attachments by expiration severity (convert thresholds from DateTime to DateOnly)
            var lightOrangeAttachments = attachments.Where(a => 
                a.ExpiryDate > DateOnly.FromDateTime(threeWeeksFromNow) && 
                a.ExpiryDate <= DateOnly.FromDateTime(fourWeeksFromNow)).ToList();

            var deepOrangeAttachments = attachments.Where(a => 
                a.ExpiryDate > DateOnly.FromDateTime(twoWeeksFromNow) && 
                a.ExpiryDate <= DateOnly.FromDateTime(threeWeeksFromNow)).ToList();

            var lightRedAttachments = attachments.Where(a => 
                a.ExpiryDate > DateOnly.FromDateTime(oneWeekFromNow) && 
                a.ExpiryDate <= DateOnly.FromDateTime(twoWeeksFromNow)).ToList();

            var redAttachments = attachments.Where(a => 
                a.ExpiryDate <= DateOnly.FromDateTime(oneWeekFromNow)).ToList();
            
            // Log counts for monitoring
            _logger.LogInformation($"Found {lightOrangeAttachments.Count} attachments expiring in 3-4 weeks");
            _logger.LogInformation($"Found {deepOrangeAttachments.Count} attachments expiring in 2-3 weeks");
            _logger.LogInformation($"Found {lightRedAttachments.Count} attachments expiring in 1-2 weeks");
            _logger.LogInformation($"Found {redAttachments.Count} attachments expiring in less than 1 week");

            // Only send email if there are attachments expiring
            if (attachments.Any())
            {
                // Get recipients from configuration
                var recipients = _configuration.GetSection("EmailNotification:Recipients")
                    .Get<List<string>>();

                if (recipients == null || !recipients.Any())
                {
                    _logger.LogWarning("No recipients configured for email notifications");
                    return;
                }

                // Build email content
                var subject = "IPCOM PIM - Attachments Expiration Notification";
                var body = GenerateEmailBody(
                    lightOrangeAttachments, 
                    deepOrangeAttachments, 
                    lightRedAttachments, 
                    redAttachments);

                // Send email
                await _emailService.SendEmailAsync(recipients, subject, body, true);
                _logger.LogInformation($"Email notification sent to {string.Join(", ", recipients)}");
            }
            else
            {
                _logger.LogInformation("No attachments expiring soon. No notification sent.");
            }
        }

        private string GenerateEmailBody(
            List<Attachment> lightOrangeAttachments,
            List<Attachment> deepOrangeAttachments,
            List<Attachment> lightRedAttachments,
            List<Attachment> redAttachments)
        {
            var body = @"
            <!DOCTYPE html>
            <html>
            <head>
                <style>
                    body { font-family: Arial, sans-serif; }
                    table { border-collapse: collapse; width: 100%; margin-top: 20px; }
                    th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
                    th { background-color: #f2f2f2; }
                    .light-orange { background-color: #FFE0B2; }
                    .deep-orange { background-color: #FFCC80; }
                    .light-red { background-color: #FFCDD2; }
                    .red { background-color: #EF9A9A; }
                    .section-title { margin-top: 20px; font-weight: bold; }
                </style>
            </head>
            <body>
                <h1>IPCOM PIM - Attachments Expiring Soon</h1>
                <p>This is an automated notification about attachments that will expire soon.</p>";

            // Add red attachments (highest priority)
            if (redAttachments.Any())
            {
                body += @"
                <div class='section-title'>Urgent Action Required - Expiring within 1 week</div>
                <table>
                    <tr>
                        <th>Product</th>
                        <th>Attachment Name</th>
                        <th>Expiry Date</th>
                    </tr>";

                foreach (var attachment in redAttachments)
                {
                    body += $@"
                    <tr class='red'>
                          <td>{attachment.Products.FirstOrDefault()?.Name}</td>
                        <td>{attachment.Name}</td>
                        <td>{attachment.ExpiryDate:yyyy-MM-dd}</td>
                    </tr>";
                }

                body += "</table>";
            }

            // Add light red attachments
            if (lightRedAttachments.Any())
            {
                body += @"
                <div class='section-title'>Expiring in 1-2 weeks</div>
                <table>
                    <tr>
                        <th>Product</th>
                        <th>Attachment Name</th>
                        <th>Expiry Date</th>
                    </tr>";

                foreach (var attachment in lightRedAttachments)
                {
                    body += $@"
                    <tr class='light-red'>
                              <td>{attachment.Products.FirstOrDefault()?.Name}</td>

                        <td>{attachment.Name}</td>
                        <td>{attachment.ExpiryDate:yyyy-MM-dd}</td>
                    </tr>";
                }

                body += "</table>";
            }

            // Add deep orange attachments
            if (deepOrangeAttachments.Any())
            {
                body += @"
                <div class='section-title'>Expiring in 2-3 weeks</div>
                <table>
                    <tr>
                        <th>Product</th>
                        <th>Attachment Name</th>
                        <th>Expiry Date</th>
                    </tr>";

                foreach (var attachment in deepOrangeAttachments)
                {
                    body += $@"
                    <tr class='deep-orange'>
        <td>{attachment.Products.FirstOrDefault()?.Name}</td>
                        <td>{attachment.Name}</td>
                        <td>{attachment.ExpiryDate:yyyy-MM-dd}</td>
                    </tr>";
                }

                body += "</table>";
            }

            // Add light orange attachments
            if (lightOrangeAttachments.Any())
            {
                body += @"
                <div class='section-title'>Expiring in 3-4 weeks</div>
                <table>
                    <tr>
                        <th>Product</th>
                        <th>Attachment Name</th>
                        <th>Expiry Date</th>
                    </tr>";

                foreach (var attachment in lightOrangeAttachments)
                {
                    body += $@"
                    <tr class='light-orange'>
                        <td>{attachment.Products.Any()}</td>
                        <td>{attachment.Name}</td>
                        <td>{attachment.ExpiryDate:yyyy-MM-dd}</td>
                    </tr>";
                }

                body += "</table>";
            }

            body += @"
                <p>Please review these attachments and take appropriate action.</p>
                <p>This is an automated message from the IPCOM PIM system.</p>
            </body>
            </html>";

            return body;
        }
    }
}