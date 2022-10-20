using AutoMapper;
using Telligent.Core.Infrastructure.Repositories;
using Telligent.Core.Infrastructure.Services;
using Telligent.Member.Application.Dtos.Campaign;
using Telligent.Member.Database;
using Telligent.Member.Domain.Campaign;

namespace Telligent.Member.Application.AppServices;

public class CampaignAppService : IAppService
{
    private readonly CampaignDbContext _context;
    private readonly IMapper _mapper;
    private readonly MemberMappingAppService _memberMappingService;
    private readonly ProspectMappingAppService _prospectMappingService;

    public CampaignAppService(
        IMapper mapper,
        CampaignDbContext context,
        MemberMappingAppService memberMappingService,
        ProspectMappingAppService prospectMappingService)
    {
        _mapper = mapper;
        _context = context;
        _memberMappingService = memberMappingService;
        _prospectMappingService = prospectMappingService;
    }

    public async Task<CampaignDto> GetByFileNoAsync(int fileNo)
    {
        var fileDetailRepository = new BaseRepository<FileDetail>(_context);

        var details = _mapper.Map<List<FileDetail>>(
            await fileDetailRepository.GetListAsync(d =>
                d.FileNo.Equals(fileNo)));

        if (details == null) return null;

        var memberIds = details.Where(m => !string.IsNullOrWhiteSpace(m.MemberId)).Select(m => Guid.Parse(m.MemberId)).ToList();
        var prospectiveCustomerNos = details.Where(m => string.IsNullOrWhiteSpace(m.MemberId) && !m.ProspectiveCustomerNo.Equals(0)).Select(m => m.ProspectiveCustomerNo).ToList();

        var prospectMappingDtos = await _prospectMappingService.GetByProspectiveCustomerNoAsync(prospectiveCustomerNos);

        return new CampaignDto
        {
            MemberIds = await _memberMappingService.GetMemberIdAsync(memberIds),
            ProspectIds = prospectMappingDtos?.Select(m => m.ProspectId).ToList()
        };
    }
}