

using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands;

public record SignIn(string Email, string Password) : ICommand;