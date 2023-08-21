namespace TaskoPhobia.Shared.Abstractions.Domain.ValueObjects.Url;

public sealed record Url
{
    public Url(string value)
    {
        var isValidUrl = Uri.TryCreate(value, UriKind.Absolute, out var result);
        if (!isValidUrl) throw new InvalidUrlException(value);

        Value = value;
    }

    public string Value { get; }
}