using BackendApi.Application.Dtos;

namespace BackendApi.Application.Interfaces;

public interface IPreventivoService
{
    IReadOnlyCollection<PreventivoDto> GetPreventivi();
    PreventivoDto? GetPreventivo(Guid id);
    IReadOnlyCollection<StatoDto> GetStati();
    bool CambiaStato(Guid preventivoId, CambiaStatoRequest request);
}
