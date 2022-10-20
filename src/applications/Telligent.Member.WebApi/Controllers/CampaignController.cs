using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.Campaign;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CampaignController : ControllerBase
{
    private readonly CampaignAppService _service;

    public CampaignController(CampaignAppService service)
    {
        _service = service;
    }

    [HttpGet("file_no")]
    public async Task<CampaignDto> GetByFileNoAsync(int fileNo)
    {
        return await _service.GetByFileNoAsync(fileNo);
    }
}