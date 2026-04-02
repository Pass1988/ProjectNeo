# backend-api

Scaffold ASP.NET Core Web API (.NET 8) per il sistema **Cambio Stato Preventivi**.

## Architettura (layer)

- `Api/`: controller HTTP.
- `Application/`: DTO, interfacce e servizi applicativi.
- `Domain/`: modelli di dominio.
- `Infrastructure/`: repository in-memory (mock iniziale).

## Endpoint iniziali

- `GET /api/preventivi`
- `GET /api/preventivi/{id}`
- `GET /api/stati`
- `POST /api/preventivi/{id}/cambia-stato`

## Scope dello scaffold

- Modelli presenti: `Preventivo`, `Stato`, `Transizione`, `Utente`, `Ruolo`, `StoricoStati`.
- Concorrenza ottimistica simulata via `VersioneRecord`.
- Active Directory **non implementata** (placeholder `IAdIdentityProvider`).

## Avvio locale

```bash
cd backend-api/src
dotnet restore
dotnet run
```

Swagger disponibile in ambiente Development.
