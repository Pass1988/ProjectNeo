namespace BackendApi.Domain.Entities;

public class StoricoStati
{
    public Guid Id { get; set; }
    public Guid PreventivoId { get; set; }
    public Guid StatoDaId { get; set; }
    public Guid StatoAId { get; set; }
    public Guid UtenteId { get; set; }
    public string? Nota { get; set; }
    public DateTime TimestampUtc { get; set; }
}
