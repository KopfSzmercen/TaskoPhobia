using System.ComponentModel.DataAnnotations;
using TaskoPhobia.Application.Commands;
using TaskoPhobia.Core.Entities;

namespace TaskoPhobia.Api.Users;

public class SignUpRequest
{
    // #CR jak wcześniej
    [Required]
    [EmailAddress]
    public string Email { get; init; }
    
    [Required]
    [MaxLength(50)]
    public string Username { get; init; }
    
    [Required]
    [MaxLength(50)]
    public string Password { get; init; }

    public SignUp ToCommand()
    {
        return new SignUp(Guid.NewGuid(), Email, Username, Password);
    }
}