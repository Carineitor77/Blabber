using Domain;

namespace Application.Common.Interfaces;

public interface ITokenService
{
    string CreateToken(AuthUser authUser);
}