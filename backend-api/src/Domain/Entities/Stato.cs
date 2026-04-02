namespace BackendApi.Domain.Entities;

public class Stato
{
    public Guid Id { get; set; }
    public string Codice { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public bool Attivo { get; set; } = true;
}
