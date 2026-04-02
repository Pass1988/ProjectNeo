# Architettura scaffold V1

## Componenti

1. **frontend-desktop**: UI operatore per elenco preventivi e cambio stato (dettaglio placeholder).
2. **backend-api**: API centrale con logiche stato e concorrenza ottimistica.
3. **database**: fonte ufficiale dello stato preventivi.
4. **sync-service**: servizio separato per lettura cartelle NAS.

## Decisioni iniziali

- Repository backend in-memory per bootstrap rapido.
- Contratti API già aderenti al dominio richiesto.
- Active Directory demandata a integrazione futura.
- Nessuna UI avanzata in questa fase.
