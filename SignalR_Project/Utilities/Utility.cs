namespace SignalR_Project.Utilities;

public class Utility
{
    public DateTime ConvertToLocalDateTime(DateTime utcDateTime)
    {
        TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
        DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);
        return localDateTime;
    }
}
