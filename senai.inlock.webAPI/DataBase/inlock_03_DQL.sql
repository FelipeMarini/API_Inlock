USE InLock_Games;
GO

-- listar todos os usu�rios (n�o mostrando senha)
SELECT U.IdUsuario, U.Email, U.IdTipoUsuario, TU.Titulo FROM Usuarios U
INNER JOIN TiposUsuario TU
ON U.IdTipoUsuario = TU.IdTipoUsuario;
GO

-- listar todos os est�dios
SELECT IdEstudio, NomeEstudio FROM Estudios; 


-- listar todos os jogos
SELECT IdJogo, NomeJogo, Descricao, DataLancamento, Valor, IdEstudio FROM Jogos;


-- listar todos os jogos com seus respectivos est�dios
SELECT J.IdJogo, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor, J.IdEstudio, E.NomeEstudio FROM Jogos J
INNER JOIN Estudios E
ON J.IdEstudio = E.IdEstudio;
GO


-- buscar e listar os est�dios com os respectivos jogos, mesmo se algum est�dio n�o tiver nenhum jogo de refer�ncia cadastrado
SELECT E.IdEstudio, E.NomeEstudio, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor FROM Estudios E
LEFT JOIN Jogos J
ON E.IdEstudio = J.IdEstudio;
GO

-- buscar e listar um usu�rio por email e senha (login)
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

-- Buscar e listar um est�dio pelo seu IdEstudio (contendo os jogos desse determinado est�dio)
SELECT E.IdEstudio, E.NomeEstudio, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor FROM Estudios E
INNER JOIN Jogos J
ON E.IdEstudio = J.IdEstudio
WHERE E.IdEstudio = 1;
GO

-- Lista os jogos de um determinado est�dio
SELECT J.IdJogo, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor, J.IdEstudio, E.NomeEstudio FROM Jogos J
INNER JOIN Estudios E
ON J.IdEstudio = E.IdEstudio
WHERE E.IdEstudio = 1;
GO