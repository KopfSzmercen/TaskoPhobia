﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Security;
using TaskoPhobia.Shared.Abstractions.Time;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TaskoPhobia.Infrastructure.Auth;

public class Authenticator : IAuthenticator
{
    private readonly string _audience;
    private readonly TimeSpan _expiry;
    private readonly string _issuer;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
    private readonly IOptions<AuthOptions> _options;
    private readonly SigningCredentials _signingCredentials;
    private readonly IClock _clock;

    public Authenticator(IOptions<AuthOptions> options, IClock clock)
    {
        _options = options;
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
        _signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
                SecurityAlgorithms.HmacSha256);
    }

    public JwtDto CreateToken(Guid userId, string role)
    {
        var now = _clock.Now();
        var expires = now.Add(_expiry);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };

        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);

        var accessToken = _jwtSecurityTokenHandler.WriteToken(jwt);

        return new JwtDto
        {
            AccessToken = accessToken
        };
    }
}