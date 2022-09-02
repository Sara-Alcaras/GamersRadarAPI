
--Criação da base de dados
CREATE DATABASE GamersRadar;

--Acessa a base de dados
USE GamersRadar;

--Criação de tabela
CREATE TABLE Usuarios
(
--Identity vai incrementar o ID automaticamente
	Id INT PRIMARY KEY IDENTITY,

--NVARCHAR que aceita até 50 caracteres
    Nome NVARCHAR(50),
	Email NVARCHAR(50),
	Senha NVARCHAR(50),
);
GO

CREATE TABLE Perfil
(
--Identity vai incrementar o ID automaticamente
	Id INT PRIMARY KEY IDENTITY,
	Biografia NVARCHAR(MAX),
	Foto NVARCHAR(MAX),
	JogosInteresse NVARCHAR(MAX),

	--DecLaração de FK
	UsuariosId INT
	FOREIGN KEY (UsuariosId) REFERENCES Usuarios(Id)
);
GO

CREATE TABLE Publicacoes
(
--Identity vai incrementar o ID automaticamente
	Id INT PRIMARY KEY IDENTITY,
	Descricao NVARCHAR(MAX),
	ImagemAnexo NVARCHAR(MAX),

	--DecLaração de FK
	PerfilId INT
	FOREIGN KEY (PerfilId) REFERENCES Perfil(Id)
);
GO

CREATE TABLE Categorias
(
--Identity vai incrementar o ID automaticamente
--NVARCHAR que aceita até 60 caracteres
	Id INT PRIMARY KEY IDENTITY,
	TipoCategoria NVARCHAR(60),

	--DecLaração de FK
	PublicacoesId INT
	FOREIGN KEY (PublicacoesId) REFERENCES Publicacoes(Id)
);
GO

CREATE TABLE Comentarios
(
--Identity vai incrementar o ID automaticamente
	Id INT PRIMARY KEY IDENTITY,
	Comentario NVARCHAR(MAX),
	DataComentario DATETIME,

	--DecLaração de FK's
	PerfilId INT
	FOREIGN KEY (PerfilId) REFERENCES Perfil(Id),

    PublicacoesId INT
	FOREIGN KEY (PublicacoesId) REFERENCES Publicacoes(Id)
);
GO

