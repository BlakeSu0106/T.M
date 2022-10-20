using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.Prospect;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProspectController : ControllerBase
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly ProspectAppService _prospectService;

    public ProspectController(
        IIdentityServerInteractionService interaction,
        ProspectAppService prospectService)
    {
        _interaction = interaction;
        _prospectService = prospectService;
    }

    [HttpGet]
    public async Task<ProspectDto> GetAsync(Guid id)
    {
        return await _prospectService.GetAsync(id);
    }

    [HttpGet("list")]
    public async Task<IList<ProspectDto>> GetListAsync(List<Guid> ids)
    {
        return await _prospectService.GetListAsync(m => ids.Contains(m.Id) && m.EntityStatus);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateProspectDto dto)
    {
        return Ok(await _prospectService.CreateAsync(dto));
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(UpdateProspectDto dto)
    {
        return Ok(await _prospectService.UpdateAsync(dto));
    }
}