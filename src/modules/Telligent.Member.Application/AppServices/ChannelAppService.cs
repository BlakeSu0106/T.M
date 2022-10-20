using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.Channels;
using Telligent.Member.Domain.Channels;
using Telligent.Member.Domain.Shared.Channels;

namespace Telligent.Member.Application.AppServices;

public class ChannelAppService : CrudAppService<Channel, ChannelDto, ChannelDto, ChannelDto>
{
    private string _companyId;
    private readonly CompanyMappingAppService _companyMappingAppService;

    public ChannelAppService(
        IRepository<Channel> repository,
        IMapper mapper,
        CompanyMappingAppService companyMappingAppService,
        IHttpContextAccessor httpContextAccessor)
        : base(repository, mapper, httpContextAccessor)
    {
        _companyMappingAppService = companyMappingAppService;

        if (httpContextAccessor.HttpContext == null) return;

        _companyId = httpContextAccessor.HttpContext.Request.Headers["Company"].ToString();

        DataInitializeAsync().Wait();

    }

    /// <summary>
    /// 取得ChannelId
    /// </summary>
    /// <param name="channelType">頻道類別</param>
    /// <returns>頻道代碼</returns>
    public async Task<IList<ChannelDto>> GetChannelIdAsync(ChannelType channelType)
    {
        if (!Guid.TryParse(_companyId, out var companyId)) throw new ValidationException("無法取得公司資訊");

        return await GetListAsync(m =>
            m.CompanyId.Equals(companyId) &&
            m.ChannelType.Equals(channelType) &&
            m.EntityStatus);
    }

    /// <summary>
    /// Gateway資料初始設定
    /// </summary>
    /// <returns></returns>
    private async Task DataInitializeAsync()
    {
        if (!string.IsNullOrEmpty(_companyId))
        {
            var mappingDto = await _companyMappingAppService.GetByOldCompanyIdAsync(_companyId);

            if (mappingDto != null)
                _companyId = mappingDto.CompanyId.ToString();
        }
    }

}