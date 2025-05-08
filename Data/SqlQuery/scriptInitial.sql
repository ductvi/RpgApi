BEGIN TRANSACTION;
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250414182120_MigracaoUmParaUm', N'9.0.3');

ALTER TABLE [Personagens] ADD [Derrotas] int NOT NULL DEFAULT 0;

ALTER TABLE [Personagens] ADD [Disputas] int NOT NULL DEFAULT 0;

ALTER TABLE [Personagens] ADD [Vitorias] int NOT NULL DEFAULT 0;

CREATE TABLE [Habilidades] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Dano] int NOT NULL,
    CONSTRAINT [PK_Habilidades] PRIMARY KEY ([Id])
);

CREATE TABLE [personagemHabilidades] (
    [PersonagemId] int NOT NULL,
    [HabilidadeId] int NOT NULL,
    CONSTRAINT [PK_personagemHabilidades] PRIMARY KEY ([PersonagemId], [HabilidadeId]),
    CONSTRAINT [FK_personagemHabilidades_Habilidades_HabilidadeId] FOREIGN KEY ([HabilidadeId]) REFERENCES [Habilidades] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_personagemHabilidades_Personagens_PersonagemId] FOREIGN KEY ([PersonagemId]) REFERENCES [Personagens] ([Id]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Dano', N'Nome') AND [object_id] = OBJECT_ID(N'[Habilidades]'))
    SET IDENTITY_INSERT [Habilidades] ON;
INSERT INTO [Habilidades] ([Id], [Dano], [Nome])
VALUES (1, 58, N'Adormecer'),
(2, 41, N'Congelar'),
(3, 37, N'Hipnotizar');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Dano', N'Nome') AND [object_id] = OBJECT_ID(N'[Habilidades]'))
    SET IDENTITY_INSERT [Habilidades] OFF;

UPDATE [Personagens] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 1;
SELECT @@ROWCOUNT;


UPDATE [Personagens] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 2;
SELECT @@ROWCOUNT;


UPDATE [Personagens] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 3;
SELECT @@ROWCOUNT;


UPDATE [Personagens] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 4;
SELECT @@ROWCOUNT;


UPDATE [Personagens] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 5;
SELECT @@ROWCOUNT;


UPDATE [Personagens] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 6;
SELECT @@ROWCOUNT;


UPDATE [Personagens] SET [Derrotas] = 0, [Disputas] = 0, [Vitorias] = 0
WHERE [Id] = 7;
SELECT @@ROWCOUNT;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'HabilidadeId', N'PersonagemId') AND [object_id] = OBJECT_ID(N'[personagemHabilidades]'))
    SET IDENTITY_INSERT [personagemHabilidades] ON;
INSERT INTO [personagemHabilidades] ([HabilidadeId], [PersonagemId])
VALUES (1, 1),
(2, 1),
(2, 2),
(2, 3),
(3, 3),
(3, 4),
(1, 5),
(2, 6),
(3, 7);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'HabilidadeId', N'PersonagemId') AND [object_id] = OBJECT_ID(N'[personagemHabilidades]'))
    SET IDENTITY_INSERT [personagemHabilidades] OFF;

CREATE INDEX [IX_personagemHabilidades_HabilidadeId] ON [personagemHabilidades] ([HabilidadeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250414190718_MigracaoMuitosParaMuitos', N'9.0.3');

COMMIT;
GO

