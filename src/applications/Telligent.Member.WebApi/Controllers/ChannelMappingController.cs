using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.ChannelMapping;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChannelMappingController : ControllerBase
{
    private readonly ChannelMappingAppService _service;

    public ChannelMappingController(ChannelMappingAppService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IList<ChannelMappingDtos>> GetListAsync(Channels dto)
    {
        return await _service.GetByOldChannelIdAsync(dto);
    }
}