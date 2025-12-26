namespace Hamkare.Common.Dto.Identity;

public class LoginResponseDto
{
    public string Token { get; set; }

    public DateTime ExpireDate { get; set; }
}