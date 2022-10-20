using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.MemberMapping;
using Telligent.Member.Domain.Members;

namespace Telligent.Member.Application.AppServices;

public class
    MemberMappingAppService : CrudAppService<MemberMapping, MemberMappingDto, MemberMappingDto, MemberMappingDto>
{
    public MemberMappingAppService(
        IRepository<MemberMapping> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(repository, mapper, httpContextAccessor)
    {
    }

    public async Task<IList<MemberMappingBaseDto>> GetByOldMemberIdAsync(MembersDto dto)
    {
        var memberMappingDtos = await GetListAsync(m =>
            dto.Members.Contains(m.OldMemberId) &&
            m.EntityStatus);

        return Mapper.Map<IList<MemberMappingBaseDto>>(memberMappingDtos);
    }

    public async Task<List<Guid>> GetMemberIdAsync(List<Guid> ids)
    {
        var memberDtos = await GetListAsync(m => ids.Contains(m.OldMemberId) && m.EntityStatus);
        //var memberMappingDtos = await GetListAsync(m =>
        //    dto.Members.Contains(m.OldMemberId) &&
        //    m.EntityStatus);

        return memberDtos?.Select(m => m.MemberId).ToList();
    }
}