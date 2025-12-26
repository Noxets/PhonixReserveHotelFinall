using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Hamkare.Utility.Helpers.Identity;

public static class HashHelper
{
    public static string HashMaker(string value)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: value,
        salt: "EMW0cMRoNFr5PKhWEUoGQieQiohy2n8rdSPyNrS1sM3DXm46VyTDndXZHqQZT0WnDoArXlwojGChLi9bNrSchEnVbdy0Jm3Z0oBSdqReX15uAknuWhJhVPw1gpagYhQanUstEhrdECaDrMRvbWuyMJ0kgISSDCP9RaksTuahcsx201gueep5BGlgUIYVaDfDIx1xJNYuafmStpNuYaZahrafycVcgyxkKl0fUGcMD6fiybqupiLPcjJbTW4sSsiM"u8.ToArray(),
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 100000,
        numBytesRequested: 256 / 8));
    }
}