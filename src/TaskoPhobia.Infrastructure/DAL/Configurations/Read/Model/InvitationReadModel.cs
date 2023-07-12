namespace TaskoPhobia.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class InvitationReadModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public UserReadModel Sender { get; set; }
    public UserReadModel Receiver { get; set; }
    public Guid ProjectId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public ProjectReadModel Project { get; set; }
}