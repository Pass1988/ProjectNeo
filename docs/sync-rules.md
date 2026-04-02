# Regole sincronizzazione NAS

## Obiettivo

Sincronizzare e monitorare il NAS senza sostituire la logica di stato centralizzata del backend.

## Frequenza

- Incrementale: ogni 10 minuti.
- Completa: notturna.
- Manuale: avvio solo admin.

## Regole automatiche conservative

- Nuova cartella valida -> `DA PREVENTIVARE`.
- PDF conforme -> `DA CONFERMARE` solo se stato corrente coerente.
- Obsolescenza temporale -> `SUPERATO` se non `CONTABILIZZATO`.

## Output

- Aggiornamento metadati tecnici/fingerprint.
- Segnalazione anomalie (cartelle rinominate/mancanti/incoerenti).
- Log dettagliato in `LogSincronizzazione`.
