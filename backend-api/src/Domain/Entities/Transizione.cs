namespace BackendApi.Domain.Entities;

public class Transizione
{
    public Guid Id { get; set; }
    public Guid StatoOrigineId { get; set; }
    public Guid StatoDestinazioneId { get; set; }
    public string? RuoloRichiesto { get; set; }
}
