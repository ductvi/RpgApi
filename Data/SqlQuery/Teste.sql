select * from Usuarios;


SELECT TOP(1) [p].[Id], [p].[Classe], [p].[Defesa], [p].[Derrotas], [p].[Disputas], [p].[Forca], [p].[FotoPersonagem], [p].[Inteligencia], [p].[Nome], [p].[PontosVida], [p].[UsuarioId], [p].[Vitorias], [a].[Id], [a].[Dano], [a].[Nome], [a].[PersonagemId], [u].[Id], [u].[DataAcesso], [u].[Email], [u].[Foto], [u].[Latitude], [u].[Longitude], [u].[PasswordHash], [u].[PasswordSalt], [u].[Perfil], [u].[Username]
      FROM [Personagens] AS [p]
      LEFT JOIN [Armas] AS [a] ON [p].[Id] = [a].[PersonagemId]
      LEFT JOIN [Usuarios] AS [u] ON [p].[UsuarioId] = [u].[Id]
      WHERE [p].[Id] = 1;



SELECT * from personagemHabilidades;