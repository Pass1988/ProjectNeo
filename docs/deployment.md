# Deployment (bozza)

## Frontend desktop

- Distribuzione manuale sui PC Windows.
- Aggiornamenti manuali in V1.

## Backend API

- Hosting interno su server Windows (IIS/Kestrel/HTTP.sys).
- Integrazione Negotiate per autenticazione Windows.

## Database

- SQL Server Express per avvio progetto.
- Possibile upgrade a SQL Server Standard.

## Sync service

- Servizio Windows separato.
- Job incrementale ogni 10 minuti.
- Full scan una volta al giorno.
