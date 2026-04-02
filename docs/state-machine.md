# Macchina a stati (V1)

## Stati base

- DA PREVENTIVARE
- DA CONFERMARE
- CONFERMATO
- CONTABILIZZATO
- ANNULLATO
- SUPERATO

## Principi

- `ANNULLATO` (manuale) distinto da `SUPERATO` (automatico).
- Le regole automatiche non devono retrocedere stati manuali avanzati.

## Transizioni standard

- DA PREVENTIVARE -> DA CONFERMARE | CONFERMATO | ANNULLATO | SUPERATO
- DA CONFERMARE -> CONFERMATO | ANNULLATO | SUPERATO
- CONFERMATO -> CONTABILIZZATO | ANNULLATO | SUPERATO
- CONTABILIZZATO -> CONFERMATO (solo ruolo autorizzato)
- ANNULLATO -> finale (salvo override admin)
- SUPERATO -> finale (salvo override admin, se abilitato)
