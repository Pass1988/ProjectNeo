using BackendApi.Domain.Entities;

namespace BackendApi.Application.Interfaces;

public interface IPreventivoRepository
{
    IReadOnlyCollection<Preventivo> GetPreventivi();
    Preventivo? GetPreventivo(Guid id);
    IReadOnlyCollection<Stato> GetStati();
    bool IsTransizioneValida(Guid statoOrigineId, Guid statoDestinazioneId);
    bool TryCambiaStato(Guid preventivoId, Guid nuovoStatoId, Guid utenteId, string? nota, byte[] expectedVersioneRecord);
}
