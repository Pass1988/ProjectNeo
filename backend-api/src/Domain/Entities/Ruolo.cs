namespace BackendApi.Domain.Entities;

public class Ruolo
{
    public Guid Id { get; set; }
    public string Codice { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
}
