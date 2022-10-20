using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.Dtos.Account;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    /// <summary>
    /// 變更密碼
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> PutPasswordAsync(UpdateAccountDto dto)
    {
        return Ok();
    }
}