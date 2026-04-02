namespace BackendApi.Application.Dtos;

public class CambiaStatoRequest
{
    public Guid NuovoStatoId { get; set; }
    public Guid UtenteId { get; set; }
    public string? Nota { get; set; }
    public string VersioneRecord { get; set; } = string.Empty;
}
