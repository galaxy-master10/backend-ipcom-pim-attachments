using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace dotnet_ipcom_pim_console.Services;

public class AttachmentExpiryWorker : BackgroundService
    {
        private readonly ILogger<AttachmentExpiryWorker> _logger;
        private readonly IAttachmentExpiryNotificationService _notificationService;
        private readonly IScheduleConfiguration _scheduleConfig;

        public AttachmentExpiryWorker(
            ILogger<AttachmentExpiryWorker> logger,
            IAttachmentExpiryNotificationService notificationService,
            IScheduleConfiguration scheduleConfig)
        {
            _logger = logger;
            _notificationService = notificationService;
            _scheduleConfig = scheduleConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Attachment Expiry Worker Service started at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Get the current time
                    var now = DateTimeOffset.Now;
                    
                    // Check if we need to run the notification at this time
                    if (ShouldRunNotification(now))
                    {
                        _logger.LogInformation("Starting scheduled notification check at: {time}", now);
                        await _notificationService.SendExpiryNotificationsAsync();
                        _logger.LogInformation("Completed scheduled notification check at: {time}", DateTimeOffset.Now);
                    }

                    // Calculate delay until next check
                    var nextCheckDelay = CalculateDelayUntilNextCheck(now);
                    
                    _logger.LogInformation("Next check scheduled for: {time}", now.Add(nextCheckDelay));
                    
                    // Wait until the next scheduled check time
                    await Task.Delay(nextCheckDelay, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing the attachment expiry notification check");
                    
                    // Wait before retrying after an error (e.g., 5 minutes)
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }
        }

        private bool ShouldRunNotification(DateTimeOffset currentTime)
        {
            // Get the scheduled hours from configuration
            var scheduledHours = _scheduleConfig.GetScheduledHours();
            
            // Get the scheduled days from configuration (0 = Sunday, 1 = Monday, etc.)
            var scheduledDays = _scheduleConfig.GetScheduledDays();
            
            // Check if the current hour is in the scheduled hours
            bool isScheduledHour = scheduledHours.Contains(currentTime.Hour);
            
            // Check if the current day is in the scheduled days
            bool isScheduledDay = scheduledDays.Contains((int)currentTime.DayOfWeek);
            
            // Only run if both the hour and day are scheduled
            return isScheduledHour && isScheduledDay;
        }

        private TimeSpan CalculateDelayUntilNextCheck(DateTimeOffset currentTime)
        {
            // Default check interval (e.g., every hour)
            return TimeSpan.FromMinutes(_scheduleConfig.CheckIntervalInMinutes);
        }
    }