using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Users;

public class BrowseUsers : PagedQuery<UserDetailsDto>
{
    public string Username { get; set; }
}