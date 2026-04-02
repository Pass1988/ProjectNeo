namespace BackendApi.Application.Dtos;

public record PreventivoDto(
    Guid Id,
    string Codice,
    string Cliente,
    Guid StatoId,
    string StatoNome,
    DateTime DataCreazioneUtc,
    string VersioneRecord);
