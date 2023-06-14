using TaskoPhobia.Shared.Abstractions.Commands;

namespace TaskoPhobia.Application.Commands.Users.SignIn;

public record SignIn(string Email, string Password) : ICommand;