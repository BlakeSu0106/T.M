using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.User;
using Telligent.Member.Domain.Users;

namespace Telligent.Member.Application.AppServices;

public class UserAppService : CrudAppService<User, UserDto, UserDto, UserDto>
{
    public UserAppService(
        IRepository<User> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(repository, mapper, httpContextAccessor)
    {
    }


}