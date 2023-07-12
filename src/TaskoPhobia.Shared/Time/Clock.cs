using TaskoPhobia.Shared.Abstractions.Time;

namespace TaskoPhobia.Shared.Time;

public class Clock : IClock
{
    public DateTime Now()
    {
        return DateTime.UtcNow;
    }

    public DateTimeOffset DateTimeOffsetNow()
    {
        return DateTimeOffset.Now;
    }
}