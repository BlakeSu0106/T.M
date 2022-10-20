using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;

namespace Telligent.Member.Application.Auth;

public class ProfileService : IProfileService
{
    private readonly UnitOfWork _uow;

    public ProfileService(UnitOfWork uow)
    {
        _uow = uow;
    }

    /// <summary>
    /// GetProfileDataAsync
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var member = await _uow.MemberRepository.GetAsync(Guid.Parse(context.Subject.GetSubjectId()));
        if (member == null) return;

        context.IssuedClaims.AddRange(new[]
        {
            new Claim(JwtClaimTypes.Id, member.Id.ToString()),
            new Claim(JwtClaimTypes.Name, member.Name),
            new Claim(JwtClaimTypes.Email, member.Email),
            new Claim("tenant", member.TenantId.ToString())
        });
    }

    /// <summary>
    /// IsActiveAsync
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}