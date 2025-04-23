namespace dotnet_ipcom_pim_console.Services;

public interface IScheduleConfiguration
{
    List<int> GetScheduledHours();
    List<int> GetScheduledDays();
    int CheckIntervalInMinutes { get; }
}