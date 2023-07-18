using TaskoPhobia.Core.Exceptions;

namespace TaskoPhobia.Core.ValueObjects;

public record InvitationStatus
{
    public InvitationStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30) throw new InvalidInvitationStatus();
        if (!AvailableInvitationStatuses.Contains(value)) throw new InvalidInvitationStatus();

        Value = value;
    }

    public string Value { get; }

    private static IEnumerable<string> AvailableInvitationStatuses { get; } =
        new[] { "pending", "accepted", "rejected" };


    public static InvitationStatus Pending()
    {
        return new InvitationStatus("pending");
    }

    public static InvitationStatus Accepted()
    {
        return new InvitationStatus("accepted");
    }

    public static InvitationStatus Rejected()
    {
        return new InvitationStatus("rejected");
    }
}