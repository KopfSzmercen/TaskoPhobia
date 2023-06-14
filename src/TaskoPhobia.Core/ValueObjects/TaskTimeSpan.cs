using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public record TaskTimeSpan
{
    public TaskTimeSpan(DateTime start, DateTime end)
    {
        if (start > end || end < DateTime.UtcNow) throw new InvalidTaskTimeSpanException();

        Start = start;
        End = end;
    }

    public DateTime Start { get; }
    public DateTime End { get; }

    public override string ToString()
    {
        return $"start: {Start:hh:mm:ss t z} end: {End:hh:mm:ss t z}";
    }
}