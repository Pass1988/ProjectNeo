# Architettura - Cambio Stato Preventivi

## Principio chiave

La **fonte ufficiale dello stato** è il database applicativo. Il NAS contiene file/cartelle ma non decide lo stato.

## Moduli

1. **Frontend desktop (Python/PySide6)**
   - Ricerca, filtri, dettaglio, cambio stato, apertura cartella NAS.
   - Invio `versioneRecord` al backend per concorrenza ottimistica.

2. **Backend API (ASP.NET Core .NET 8)**
   - Autenticazione Windows/AD.
   - Autorizzazione per ruoli applicativi.
   - Validazione transizioni stato.
   - Persistenza, storico e audit log.

3. **Database SQL Server**
   - Entità: Preventivi, Stati, Transizioni, Utenti/Ruoli, Storico, Log sincronizzazione, File indicizzati.

4. **Sync service NAS (servizio Windows)**
   - Scansione incrementale periodica + full scan notturna.
   - Rilevazione anomalie e riallineamento tecnico.
   - Nessun bypass della logica di cambio stato manuale del backend.

## Flussi principali

- **Consultazione**: frontend legge dal backend (non dal NAS diretto).
- **Cambio stato**: backend valida ruoli/transizioni/versione e scrive storico.
- **Sincronizzazione**: processo separato aggiorna metadati tecnici e segnala anomalie.
