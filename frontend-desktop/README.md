# frontend-desktop

Client desktop Python + PySide6 (scaffold) per consultazione preventivi.

## Funzionalità V1

- Finestra principale.
- Tabella preventivi.
- Filtro per stato.
- Pulsante **Apri dettaglio** (placeholder).
- Integrazione API con fallback a dati mock se backend non raggiungibile.

## Avvio locale

```bash
cd frontend-desktop
python3 -m venv .venv
source .venv/bin/activate  # su Windows: .venv\Scripts\activate
pip install -r requirements.txt
python app/main.py
```

## Configurazione

L'`ApiClient` punta a `http://localhost:5000`.
Per questa prima versione è accettabile il fallback mock locale.
