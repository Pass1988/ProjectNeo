# API Spec (V1)

## Preventivi

- `GET /api/preventivi`
- `GET /api/preventivi/{id}`
- `GET /api/preventivi?stato=...&testo=...`
- `GET /api/preventivi/{id}/storico`
- `GET /api/preventivi/{id}/stati-consentiti`

## Cambio stato

- `POST /api/preventivi/{id}/cambia-stato`

Body:

```json
{
  "nuovoStatoId": 0,
  "nota": "stringa opzionale",
  "versioneRecord": 0
}
```

## Stati

- `GET /api/stati`

## Admin

- `GET /api/admin/log-sincronizzazione`
- `GET /api/admin/utenti`
- `GET /api/admin/ruoli`
- `GET /api/admin/transizioni`
- `POST /api/admin/sincronizzazione/avvia`
- `GET /api/admin/sincronizzazione/stato`
