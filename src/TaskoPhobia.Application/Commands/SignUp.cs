using System.Text.Json.Serialization;
using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands;

public record SignUp(Guid UserId, string Email, string Username, string Password) : ICommand;