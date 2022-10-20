using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.Channels;
using Telligent.Member.Domain.Shared.Channels;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChannelController : ControllerBase
{
    private readonly ChannelAppService _service;

    public ChannelController(ChannelAppService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ChannelDto> GetAsync(Guid id)
    {
        return await _service.GetAsync(id);
    }

    /// <summary>
    /// 取得ChannelId
    /// </summary>
    /// <param name="channelType">渠道類別</param>
    /// <returns>頻道代碼</returns>
    [HttpGet("ChannelId")]
    public async Task<IActionResult> GetChannelIdAsync(ChannelType channelType)
    {
        return Ok(await _service.GetChannelIdAsync(channelType));
    }

}