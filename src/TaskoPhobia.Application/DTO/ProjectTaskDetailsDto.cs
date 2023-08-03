using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.DTO;

public class ProjectTaskDetailsDto : IQuery<ProjectTaskDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public IEnumerable<UserDto> Assignees { get; set; }
}