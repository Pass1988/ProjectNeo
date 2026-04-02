using BackendApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Api.Controllers;

[ApiController]
[Route("api/stati")]
public class StatiController(IPreventivoService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetStati() => Ok(service.GetStati());
}
