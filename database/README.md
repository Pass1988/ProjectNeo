# database

Contiene schema SQL Server e script iniziali per il progetto **Cambio Stato Preventivi**.

## Contenuto

- `schema/001_initial.sql`: creazione tabelle, vincoli FK, concorrenza ottimistica con `ROWVERSION` e seed iniziale stati.

## Avvio rapido

1. Creare un database SQL Server (es. `CambioStatoPreventivi`).
2. Eseguire lo script:

```sql
:r .\database\schema\001_initial.sql
```

oppure copiarne il contenuto in SSMS/Azure Data Studio ed eseguirlo.

## Note

- Il database è la fonte ufficiale dello stato dei preventivi.
- AD, full-text search e logiche automatiche avanzate sono fuori scope di questo scaffold.
