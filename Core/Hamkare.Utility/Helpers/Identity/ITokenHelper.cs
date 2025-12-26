using System.Security.Claims;

namespace Hamkare.Utility.Helpers.Identity;

public interface ITokenHelper
{
    string BuildToken(IEnumerable<Claim> authClaims);
}