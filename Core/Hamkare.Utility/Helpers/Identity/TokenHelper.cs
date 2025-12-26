using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hamkare.Utility.Helpers.Identity;

public class TokenHelper : ITokenHelper
{
    public string BuildToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey("EMW0cMRoNFr5PKhWEUoGQieQiohy2n8rdSPyNrS1sM3DXm46VyTDndXZHqQZT0WnDoArXlwojGChLi9bNrSchEnVbdy0Jm3Z0oBSdqReX15uAknuWhJhVPw1gpagYhQanUstEhrdECaDrMRvbWuyMJ0kgISSDCP9RaksTuahcsx201gueep5BGlgUIYVaDfDIxYAAliuafmStpNuYaZahrafycVcgyxkKl0fUGcMD6fiybqupiLPcjJbTW4sSsiM"u8.ToArray());

        var token = new JwtSecurityToken(
        "https://localhost",
        "https://localhost:4200",
        expires: DateTime.Now.AddHours(3),
        claims: authClaims,
        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}