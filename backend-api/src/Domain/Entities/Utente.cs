namespace BackendApi.Domain.Entities;

public class Utente
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public bool Attivo { get; set; } = true;
}
