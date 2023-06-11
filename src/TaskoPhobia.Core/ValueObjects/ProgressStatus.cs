using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;



public record ProgressStatus
{
    public static IEnumerable<string> AvailableProgressStatuses { get; } = new[] {"in-progress", "finished"};
    
    public string Value { get; }

    public ProgressStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            throw new InvalidProgressStatusException();
        }

        if (!AvailableProgressStatuses.Contains(value))
        {
            throw new InvalidProgressStatusException();
        }

        Value = value;
    }

    public static ProgressStatus InProgress() => new ProgressStatus("in-progress");
    public static ProgressStatus Finished() => new ProgressStatus("finished");

    public static implicit operator ProgressStatus(string value) => new(value);
    public static implicit operator string(ProgressStatus value) => value.Value;
    public override string ToString() => Value;
}