namespace BackendApi.Domain.Entities;

public class Preventivo
{
    public Guid Id { get; set; }
    public string Codice { get; set; } = string.Empty;
    public string Cliente { get; set; } = string.Empty;
    public Guid StatoId { get; set; }
    public DateTime DataCreazioneUtc { get; set; }
    public byte[] VersioneRecord { get; set; } = Array.Empty<byte>();
}
