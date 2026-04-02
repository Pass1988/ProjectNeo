# Permessi e ruoli

## Autenticazione

- Windows Authentication / Active Directory.

## Mapping AD -> ruoli applicativi

- `Preventivi_Admin`
- `Preventivi_Operatori_Standard`
- `Preventivi_Operatori_Avanzati`
- `Preventivi_ReadOnly`

## Ruoli

- **Admin**: pieno controllo (stati, transizioni, utenti, log, sync).
- **Operatore standard**: gestione pipeline base (no contabilizzazione).
- **Operatore avanzato/contabilità**: gestione `CONTABILIZZATO` e ritorno a `CONFERMATO`.
- **ReadOnly**: sola consultazione.
