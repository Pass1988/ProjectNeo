# Cambio Stato Preventivi

Scaffold iniziale del sistema con architettura modulare:

- `frontend-desktop` (Python + PySide6)
- `backend-api` (ASP.NET Core Web API .NET 8)
- `database` (SQL Server schema iniziale)
- `sync-service` (servizio separato per NAS)
- `docs` (documentazione di progetto)

## Note su specifica

Il file `PROJECT_SPEC.md` non è presente nel repository al momento della generazione scaffold.
Sono state applicate le richieste ricevute nel task e documentate le scelte nei README dei componenti.

## Avvio rapido locale

### 1) Database

Eseguire `database/schema/001_initial.sql` su SQL Server.

### 2) Backend API

```bash
cd backend-api/src
dotnet restore
dotnet run
```

### 3) Frontend desktop

```bash
cd frontend-desktop
python3 -m venv .venv
source .venv/bin/activate
pip install -r requirements.txt
python app/main.py
```

### 4) Sync service

```bash
cd sync-service/src
dotnet restore
dotnet run
```
