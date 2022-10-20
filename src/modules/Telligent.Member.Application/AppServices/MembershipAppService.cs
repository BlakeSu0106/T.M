using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.Membership;
using Telligent.Member.Domain.Members;

namespace Telligent.Member.Application.AppServices;

public class MembershipAppService : CrudAppService<Membership, MembershipDto, CreateMembershipDto, UpdateMembershipDto>
{
    public MembershipAppService(IRepository<Membership> repository, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(repository, mapper, httpContextAccessor)
    {
    }

    /// <summary>
    /// 取得公司下的所有會籍資料（並按照會籍排序）
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    public async Task<IList<MembershipDto>> GetListByCompanyAsync(Guid companyId)
    {
        var result = new List<MembershipDto>();

        var list = await GetListAsync(m =>
            m.TenantId.Equals(Payload.TenantId) &&
            m.CompanyId.Equals(companyId) &&
            m.EntityStatus);

        Guid? previousMembershipId = null;

        do
        {
            var dto = list.FirstOrDefault(m => m.PreviousMembershipId.Equals(previousMembershipId));
            if (dto == null)
                break;

            result.Add(dto);
            previousMembershipId = dto.Id;
        } while (true);

        return result;
    }

    public override async Task<MembershipDto> SetAdditionPropertiesAsync(MembershipDto dto)
    {
        if (dto?.PreviousMembershipId == null)
            return await base.SetAdditionPropertiesAsync(dto);

        dto.PreviousMembership = await GetAsync((Guid) dto.PreviousMembershipId);

        return dto;
    }
}