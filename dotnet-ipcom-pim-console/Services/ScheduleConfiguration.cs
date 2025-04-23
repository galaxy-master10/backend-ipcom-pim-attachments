using Microsoft.Extensions.Configuration;

namespace dotnet_ipcom_pim_console.Services;

public class ScheduleConfiguration : IScheduleConfiguration
{
    private readonly IConfiguration _configuration;

    public ScheduleConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<int> GetScheduledHours()
    {
        var hoursString = _configuration["ScheduleSettings:ScheduledHours"];
            
        if (string.IsNullOrEmpty(hoursString))
        {
            // Default to run once a day at 8 AM
            return new List<int> { 8 };
        }
            
        return hoursString.Split(',')
            .Select(h => int.Parse(h.Trim()))
            .ToList();
    }

    public List<int> GetScheduledDays()
    {
        var daysString = _configuration["ScheduleSettings:ScheduledDays"];
            
        if (string.IsNullOrEmpty(daysString))
        {
            // Default to run every weekday (Monday to Friday)
            return new List<int> { 1, 2, 3, 4, 5 };
        }
            
        return daysString.Split(',')
            .Select(d => int.Parse(d.Trim()))
            .ToList();
    }

    public int CheckIntervalInMinutes
    {
        get
        {
            if (int.TryParse(_configuration["ScheduleSettings:CheckIntervalInMinutes"], out int interval))
            {
                return interval;
            }
            // Default to check every 60 minutes
            return 60;
        }
    }
}