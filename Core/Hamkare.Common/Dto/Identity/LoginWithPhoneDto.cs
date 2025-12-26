namespace Hamkare.Common.Dto.Identity;

public class LoginWithPhoneDto
{
    public string PhoneNumber { get; set; }

    public string Code { get; set; }

    public int ExpireMinutes { get; set; }
}