using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.MemberMapping;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberMappingController : ControllerBase
{
    private readonly MemberMappingAppService _service;

    public MemberMappingController(MemberMappingAppService service)
    {
        _service = service;
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetListAsync(MembersDto dto)
    {
        return Ok(await _service.GetByOldMemberIdAsync(dto));
    }
}