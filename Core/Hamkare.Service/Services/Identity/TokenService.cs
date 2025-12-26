using Hamkare.Common.Interface.Services.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hamkare.Service.Services.Identity;

public class TokenService : ITokenService
{
    public string BuildToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey("EMW0cMRoNFr5PKhWEUoGQieQiohy2n8rdSPyNrS1sM3DXm46VyTDndXZHqQZT0WnDoArXlwojGChLi9bNrSchEnVbdy0Jm3Z0oBSdqReX15uAknuWhJhVPw1gpagYhQanUstEhrdECaDrMRvbWuyMJ0kgISSDCP9RaksTuahcsx201gueep5BGlgUIYVaDfDIxYAAliuafmStpNuYaZahrafycVcgyxkKl0fUGcMD6fiybqupiLPcjJbTW4sSsiM"u8.ToArray());

        var token = new JwtSecurityToken(
        issuer: "https://localhost",
        audience: "https://localhost",
        expires: DateTime.Now.AddHours(3),
        claims: authClaims,
        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}