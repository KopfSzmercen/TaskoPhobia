using Microsoft.AspNetCore.Http;
using TaskoPhobia.Application.DTO;
using TaskoPhobia.Application.Security;

namespace TaskoPhobia.Infrastructure.Auth;

internal sealed class HttpContextTokenStorage : ITokenStorage
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string TokenKey = "jwt";

    public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(JwtDto jwt) => _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);

    public JwtDto Get()
    {
        if (_httpContextAccessor is null) return null;
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt)) return jwt as JwtDto;

        return null;
    }
}