USE InLock_Games;
GO

-- listar todos os usuários (não mostrando senha)
SELECT U.IdUsuario, U.Email, U.IdTipoUsuario, TU.Titulo FROM Usuarios U
INNER JOIN TiposUsuario TU
ON U.IdTipoUsuario = TU.IdTipoUsuario;
GO

-- listar todos os estúdios
SELECT IdEstudio, NomeEstudio FROM Estudios; 


-- listar todos os jogos
SELECT IdJogo, NomeJogo, Descricao, DataLancamento, Valor, IdEstudio FROM Jogos;


-- listar todos os jogos com seus respectivos estúdios
SELECT J.IdJogo, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor, J.IdEstudio, E.NomeEstudio FROM Jogos J
INNER JOIN Estudios E
ON J.IdEstudio = E.IdEstudio;
GO


-- buscar e listar os estúdios com os respectivos jogos, mesmo se algum estúdio não tiver nenhum jogo de referência cadastrado
SELECT E.IdEstudio, E.NomeEstudio, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor FROM Estudios E
LEFT JOIN Jogos J
ON E.IdEstudio = J.IdEstudio;
GO

-- buscar e listar um usuário por email e senha (login)
SELECT IdUsuario, Email, U.IdTipoUsuario, TU.Titulo FROM Usuarios U
INNER JOIN TiposUsuario TU
ON U.IdTipoUsuario = TU.IdTipoUsuario
WHERE Email = 'admin@admin.com' AND Senha = 'admin';
GO

-- buscar e listar um jogo pelo seu IdJogo
SELECT J.IdJogo, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor, J.IdEstudio, E.NomeEstudio FROM Jogos J
INNER JOIN Estudios E
ON J.IdEstudio = E.IdEstudio
WHERE J.IdJogo = 1;
GO

-- Buscar e listar um estúdio pelo seu IdEstudio (contendo os jogos desse determinado estúdio)
SELECT E.IdEstudio, E.NomeEstudio, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor FROM Estudios E
INNER JOIN Jogos J
ON E.IdEstudio = J.IdEstudio
WHERE E.IdEstudio = 1;
GO

-- Lista os jogos de um determinado estúdio
SELECT J.IdJogo, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor, J.IdEstudio, E.NomeEstudio FROM Jogos J
INNER JOIN Estudios E
ON J.IdEstudio = E.IdEstudio
WHERE E.IdEstudio = 1;
GO