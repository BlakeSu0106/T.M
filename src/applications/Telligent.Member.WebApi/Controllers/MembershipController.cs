using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Telligent.Member.Application.AppServices;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembershipController : ControllerBase
{
    private const string ApiTag = "Membership - 會員會籍定義檔";

    private readonly MembershipAppService _membershipAppService;

    public MembershipController(MembershipAppService membershipAppService)
    {
        _membershipAppService = membershipAppService;
    }

    /// <summary>
    /// 取得公司下的所有會籍資料（已排序）
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    [SwaggerOperation(Tags = new[] { ApiTag })]
    [HttpGet("company")]
    public async Task<IActionResult> GetListByCompanyAsync(Guid companyId)
    {
        return Ok(await _membershipAppService.GetListByCompanyAsync(companyId));
    }

    /// <summary>
    /// 取得單一會籍資料
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [SwaggerOperation(Tags = new[] { ApiTag })]
    [HttpGet]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(await _membershipAppService.GetAsync(id));
    }
}