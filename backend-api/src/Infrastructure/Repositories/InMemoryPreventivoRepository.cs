using BackendApi.Application.Interfaces;
using BackendApi.Domain.Entities;

namespace BackendApi.Infrastructure.Repositories;

public class InMemoryPreventivoRepository : IPreventivoRepository
{
    private readonly object _lock = new();

    private readonly List<Stato> _stati =
    [
        new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Codice = "BOZZA", Nome = "Bozza" },
        new() { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Codice = "IN_REVISIONE", Nome = "In revisione" },
        new() { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Codice = "APPROVATO", Nome = "Approvato" },
        new() { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Codice = "RIFIUTATO", Nome = "Rifiutato" }
    ];

    private readonly List<Transizione> _transizioni =
    [
        new() { Id = Guid.NewGuid(), StatoOrigineId = Guid.Parse("11111111-1111-1111-1111-111111111111"), StatoDestinazioneId = Guid.Parse("22222222-2222-2222-2222-222222222222") },
        new() { Id = Guid.NewGuid(), StatoOrigineId = Guid.Parse("22222222-2222-2222-2222-222222222222"), StatoDestinazioneId = Guid.Parse("33333333-3333-3333-3333-333333333333") },
        new() { Id = Guid.NewGuid(), StatoOrigineId = Guid.Parse("22222222-2222-2222-2222-222222222222"), StatoDestinazioneId = Guid.Parse("44444444-4444-4444-4444-444444444444") }
    ];

    private readonly List<Preventivo> _preventivi =
    [
        new()
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Codice = "PRV-2026-0001",
            Cliente = "Acme S.p.A.",
            StatoId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            DataCreazioneUtc = DateTime.UtcNow.AddDays(-5),
            VersioneRecord = BitConverter.GetBytes(1L)
        },
        new()
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Codice = "PRV-2026-0002",
            Cliente = "Globex S.r.l.",
            StatoId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            DataCreazioneUtc = DateTime.UtcNow.AddDays(-2),
            VersioneRecord = BitConverter.GetBytes(3L)
        }
    ];

    private readonly List<StoricoStati> _storico = [];

    public IReadOnlyCollection<Preventivo> GetPreventivi() => _preventivi.ToArray();

    public Preventivo? GetPreventivo(Guid id) => _preventivi.FirstOrDefault(x => x.Id == id);

    public IReadOnlyCollection<Stato> GetStati() => _stati.ToArray();

    public bool IsTransizioneValida(Guid statoOrigineId, Guid statoDestinazioneId) =>
        _transizioni.Any(t => t.StatoOrigineId == statoOrigineId && t.StatoDestinazioneId == statoDestinazioneId);

    public bool TryCambiaStato(Guid preventivoId, Guid nuovoStatoId, Guid utenteId, string? nota, byte[] expectedVersioneRecord)
    {
        lock (_lock)
        {
            var preventivo = _preventivi.FirstOrDefault(x => x.Id == preventivoId);
            if (preventivo is null)
            {
                return false;
            }

            if (!preventivo.VersioneRecord.SequenceEqual(expectedVersioneRecord))
            {
                return false;
            }

            if (!IsTransizioneValida(preventivo.StatoId, nuovoStatoId))
            {
                return false;
            }

            var statoDa = preventivo.StatoId;
            preventivo.StatoId = nuovoStatoId;

            var currentVersion = BitConverter.ToInt64(preventivo.VersioneRecord);
            preventivo.VersioneRecord = BitConverter.GetBytes(currentVersion + 1);

            _storico.Add(new StoricoStati
            {
                Id = Guid.NewGuid(),
                PreventivoId = preventivo.Id,
                StatoDaId = statoDa,
                StatoAId = nuovoStatoId,
                UtenteId = utenteId,
                Nota = nota,
                TimestampUtc = DateTime.UtcNow
            });

            return true;
        }
    }
}
