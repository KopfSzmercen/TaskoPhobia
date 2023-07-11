using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public record ProgressStatus
{
    public ProgressStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30) throw new InvalidProgressStatusException();

        if (!AvailableProgressStatuses.Contains(value)) throw new InvalidProgressStatusException();

        Value = value;
    }

    private static IEnumerable<string> AvailableProgressStatuses { get; } = new[] { "in-progress", "finished" };

    public string Value { get; }

    public static ProgressStatus InProgress()
    {
        return new ProgressStatus("in-progress");
    }

    public static ProgressStatus Finished()
    {
        return new ProgressStatus("finished");
    }

    public static implicit operator ProgressStatus(string value)
    {
        return new ProgressStatus(value);
    }

    public static implicit operator string(ProgressStatus value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}