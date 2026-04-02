using BackendApi.Application.Dtos;
using BackendApi.Application.Interfaces;

namespace BackendApi.Application.Services;

public class PreventivoService(IPreventivoRepository repository) : IPreventivoService
{
    public IReadOnlyCollection<PreventivoDto> GetPreventivi()
    {
        var stati = repository.GetStati().ToDictionary(s => s.Id, s => s.Nome);
        return repository.GetPreventivi()
            .Select(p => new PreventivoDto(
                p.Id,
                p.Codice,
                p.Cliente,
                p.StatoId,
                stati.GetValueOrDefault(p.StatoId, "Sconosciuto"),
                p.DataCreazioneUtc,
                Convert.ToBase64String(p.VersioneRecord)))
            .ToArray();
    }

    public PreventivoDto? GetPreventivo(Guid id)
    {
        var p = repository.GetPreventivo(id);
        if (p is null)
        {
            return null;
        }

        var statoNome = repository.GetStati().FirstOrDefault(s => s.Id == p.StatoId)?.Nome ?? "Sconosciuto";

        return new PreventivoDto(
            p.Id,
            p.Codice,
            p.Cliente,
            p.StatoId,
            statoNome,
            p.DataCreazioneUtc,
            Convert.ToBase64String(p.VersioneRecord));
    }

    public IReadOnlyCollection<StatoDto> GetStati() =>
        repository.GetStati().Select(s => new StatoDto(s.Id, s.Codice, s.Nome)).ToArray();

    public bool CambiaStato(Guid preventivoId, CambiaStatoRequest request)
    {
        var expected = string.IsNullOrWhiteSpace(request.VersioneRecord)
            ? Array.Empty<byte>()
            : Convert.FromBase64String(request.VersioneRecord);

        return repository.TryCambiaStato(preventivoId, request.NuovoStatoId, request.UtenteId, request.Nota, expected);
    }
}
