using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.Member;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberController : ControllerBase
{
    private readonly MemberAppService _service;

    public MemberController(MemberAppService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<MemberDto> GetAsync(Guid id)
    {
        return await _service.GetAsync(id);
    }

    [HttpGet("list")]
    public async Task<IList<MemberDto>> GetListAsync(List<Guid> ids)
    {
        return await _service.GetListAsync(m => ids.Contains(m.Id) && m.EntityStatus);
    }
}