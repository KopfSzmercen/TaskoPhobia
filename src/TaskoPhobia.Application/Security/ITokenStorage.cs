using TaskoPhobia.Application.DTO;

namespace TaskoPhobia.Application.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}