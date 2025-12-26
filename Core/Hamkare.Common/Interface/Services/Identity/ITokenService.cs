using System.Security.Claims;

namespace Hamkare.Common.Interface.Services.Identity;

public interface ITokenService
{
    string BuildToken(IEnumerable<Claim> authClaims);
}