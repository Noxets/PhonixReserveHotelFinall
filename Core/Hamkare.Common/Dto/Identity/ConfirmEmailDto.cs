namespace Hamkare.Common.Dto.Identity;

public class ConfirmEmailDto(string token, string email)
{

    public string Token { get; set; } = token;

    public string Email { get; set; } = email;
}