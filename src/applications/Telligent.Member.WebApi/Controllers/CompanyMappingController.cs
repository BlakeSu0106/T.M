using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;
using Telligent.Member.Application.Dtos.CompanyMapping;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyMappingController : ControllerBase
{
    private readonly CompanyMappingAppService _service;

    public CompanyMappingController(CompanyMappingAppService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<CompanyMappingDto> GetAsync(string id)
    {
        return await _service.GetByOldCompanyIdAsync(id);
    }
}