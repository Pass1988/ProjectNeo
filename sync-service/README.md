# sync-service

Servizio separato di sincronizzazione NAS (scaffold iniziale).

## Funzionalità V1

- Legge `Sync:RootPath` da configurazione.
- Elenca in log le cartelle trovate nel path.
- Non implementa ancora indicizzazione, regole avanzate o automazioni.

## Configurazione

File: `src/appsettings.json`

```json
{
  "Sync": {
    "RootPath": "C:/Nas/Preventivi"
  }
}
```

## Avvio locale

```bash
cd sync-service/src
dotnet restore
dotnet run
```
