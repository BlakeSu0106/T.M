using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Member.Application.Dtos.User;

public class UserDto : EntityDto
{
    public string UserId { get; set; }
    public string Name { get; set; }
}