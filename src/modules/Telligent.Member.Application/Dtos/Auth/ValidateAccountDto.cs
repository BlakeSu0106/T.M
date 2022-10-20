namespace Telligent.Member.Application.Dtos.Auth;

public class ValidateAccountDto
{
    public Guid CompanyId { get; set; }

    public string UserId { get; set; }
}