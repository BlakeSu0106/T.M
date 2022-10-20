using Microsoft.AspNetCore.Mvc;
using Telligent.Member.Application.AppServices;

namespace Telligent.Member.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserAppService _service;

    public UserController(UserAppService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(await _service.GetAsync(id));
    }
}