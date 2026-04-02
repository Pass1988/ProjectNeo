/*
  Schema iniziale SQL Server - Cambio Stato Preventivi
  NOTE:
  - Il database è fonte ufficiale dello stato.
  - VersioneRecord (rowversion) abilita concorrenza ottimistica.
*/

CREATE TABLE Ruoli (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Codice NVARCHAR(50) NOT NULL UNIQUE,
    Nome NVARCHAR(100) NOT NULL,
    DataCreazioneUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE Utenti (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Username NVARCHAR(150) NOT NULL UNIQUE,
    DisplayName NVARCHAR(200) NOT NULL,
    Attivo BIT NOT NULL DEFAULT 1,
    DataCreazioneUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE UtentiRuoli (
    UtenteId UNIQUEIDENTIFIER NOT NULL,
    RuoloId UNIQUEIDENTIFIER NOT NULL,
    DataAssegnazioneUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_UtentiRuoli PRIMARY KEY (UtenteId, RuoloId),
    CONSTRAINT FK_UtentiRuoli_Utenti FOREIGN KEY (UtenteId) REFERENCES Utenti(Id),
    CONSTRAINT FK_UtentiRuoli_Ruoli FOREIGN KEY (RuoloId) REFERENCES Ruoli(Id)
);

CREATE TABLE Stati (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Codice NVARCHAR(50) NOT NULL UNIQUE,
    Nome NVARCHAR(100) NOT NULL,
    Descrizione NVARCHAR(500) NULL,
    Ordinamento INT NOT NULL,
    Attivo BIT NOT NULL DEFAULT 1
);

CREATE TABLE TransizioniStato (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    StatoOrigineId UNIQUEIDENTIFIER NOT NULL,
    StatoDestinazioneId UNIQUEIDENTIFIER NOT NULL,
    RuoloRichiestoId UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Transizioni_StatoOrigine FOREIGN KEY (StatoOrigineId) REFERENCES Stati(Id),
    CONSTRAINT FK_Transizioni_StatoDestinazione FOREIGN KEY (StatoDestinazioneId) REFERENCES Stati(Id),
    CONSTRAINT FK_Transizioni_Ruolo FOREIGN KEY (RuoloRichiestoId) REFERENCES Ruoli(Id),
    CONSTRAINT UQ_Transizioni UNIQUE (StatoOrigineId, StatoDestinazioneId, RuoloRichiestoId)
);

CREATE TABLE Preventivi (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Codice NVARCHAR(50) NOT NULL UNIQUE,
    Cliente NVARCHAR(200) NOT NULL,
    StatoId UNIQUEIDENTIFIER NOT NULL,
    Importo DECIMAL(18,2) NULL,
    DataCreazioneUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    DataUltimaModificaUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    VersioneRecord ROWVERSION NOT NULL,
    CONSTRAINT FK_Preventivi_Stati FOREIGN KEY (StatoId) REFERENCES Stati(Id)
);

CREATE TABLE StoricoStati (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    PreventivoId UNIQUEIDENTIFIER NOT NULL,
    StatoDaId UNIQUEIDENTIFIER NOT NULL,
    StatoAId UNIQUEIDENTIFIER NOT NULL,
    UtenteId UNIQUEIDENTIFIER NOT NULL,
    Nota NVARCHAR(1000) NULL,
    TimestampUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Storico_Preventivo FOREIGN KEY (PreventivoId) REFERENCES Preventivi(Id),
    CONSTRAINT FK_Storico_StatoDa FOREIGN KEY (StatoDaId) REFERENCES Stati(Id),
    CONSTRAINT FK_Storico_StatoA FOREIGN KEY (StatoAId) REFERENCES Stati(Id),
    CONSTRAINT FK_Storico_Utente FOREIGN KEY (UtenteId) REFERENCES Utenti(Id)
);

CREATE TABLE LogSincronizzazione (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    DataEventoUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Livello NVARCHAR(20) NOT NULL,
    Messaggio NVARCHAR(2000) NOT NULL,
    Dettaglio NVARCHAR(MAX) NULL,
    VersioneRecord ROWVERSION NOT NULL
);

CREATE TABLE FileIndicizzati (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    PreventivoId UNIQUEIDENTIFIER NULL,
    PercorsoFile NVARCHAR(1000) NOT NULL,
    HashFile NVARCHAR(128) NULL,
    DataIndicizzazioneUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    VersioneRecord ROWVERSION NOT NULL,
    CONSTRAINT FK_FileIndicizzati_Preventivo FOREIGN KEY (PreventivoId) REFERENCES Preventivi(Id)
);

-- Seed stati iniziali
INSERT INTO Stati (Id, Codice, Nome, Descrizione, Ordinamento, Attivo)
VALUES
 ('11111111-1111-1111-1111-111111111111', 'BOZZA', 'Bozza', 'Preventivo appena creato', 10, 1),
 ('22222222-2222-2222-2222-222222222222', 'IN_REVISIONE', 'In revisione', 'In attesa di validazione', 20, 1),
 ('33333333-3333-3333-3333-333333333333', 'APPROVATO', 'Approvato', 'Preventivo approvato', 30, 1),
 ('44444444-4444-4444-4444-444444444444', 'RIFIUTATO', 'Rifiutato', 'Preventivo rifiutato', 40, 1);
