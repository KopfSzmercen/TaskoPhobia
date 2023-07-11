namespace TaskoPhobia.Application.DTO;

public class SentInvitationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto Receiver { get; set; }
}