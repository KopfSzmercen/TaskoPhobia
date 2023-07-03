using Microsoft.AspNetCore.Identity;
using TaskoPhobia.Application.Security;

namespace TaskoPhobia.Infrastructure.Security;

internal sealed class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<object> _passwordHasher;

    public PasswordManager(IPasswordHasher<object> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password)
    {
        return _passwordHasher.HashPassword(new object(), password);
    }

    public bool Validate(string password, string securedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(new object(), securedPassword, password) is
            PasswordVerificationResult.Success;
    }
}