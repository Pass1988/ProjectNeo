using BackendApi.Application.Dtos;
using BackendApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Api.Controllers;

[ApiController]
[Route("api/preventivi")]
public class PreventiviController(IPreventivoService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetPreventivi() => Ok(service.GetPreventivi());

    [HttpGet("{id:guid}")]
    public IActionResult GetPreventivo(Guid id)
    {
        var preventivo = service.GetPreventivo(id);
        return preventivo is null ? NotFound() : Ok(preventivo);
    }

    [HttpPost("{id:guid}/cambia-stato")]
    public IActionResult CambiaStato(Guid id, [FromBody] CambiaStatoRequest request)
    {
        var ok = service.CambiaStato(id, request);
        return ok ? NoContent() : Conflict(new { message = "Transizione non valida o concorrenza rilevata." });
    }
}
