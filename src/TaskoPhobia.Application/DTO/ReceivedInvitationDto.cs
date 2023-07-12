namespace TaskoPhobia.Application.DTO;

public class ReceivedInvitationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto Sender { get; set; }

    public ProjectDto Project { get; set; }
}