using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly CompanyAppService _companyAppService;

    public CompanyController(CompanyAppService companyAppService)
    {
        _companyAppService = companyAppService;
    }

    /// <summary>
    /// 取公司資料
    /// </summary>
    /// <param name="id">公司識別碼</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(await _companyAppService.GetAsync(id));
    }
}