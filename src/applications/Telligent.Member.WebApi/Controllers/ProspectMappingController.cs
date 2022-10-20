using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.ProspectMapping;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProspectMappingController : ControllerBase
{
    private readonly ProspectMappingAppService _service;

    public ProspectMappingController(ProspectMappingAppService service)
    {
        _service = service;
    }

    [HttpPost("list")]
    public async Task<IList<ProspectMappingDtos>> GetByOldProspecIdAsync(ProspectsDto dto)
    {
        return await _service.GetByOldProspecIdAsync(dto);
    }
}