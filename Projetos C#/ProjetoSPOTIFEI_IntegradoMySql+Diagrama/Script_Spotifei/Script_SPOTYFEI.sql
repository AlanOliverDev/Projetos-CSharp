CREATE DATABASE IF NOT EXISTS Spotifei;
USE Spotifei;

SHOW TABLES;

select * from album_tb
,artista_tb
avaliacao_tb,
biblioteca_tb,
conteudo_tb,
historico_reproducao_tb,
musica_tb,
pagamento_tb,
perfil_usuario_tb,
plano_assinatura_tb,
playlist_conteudo_tb,
playlist_tb,
podcast_tb,
usuario_tb;



CREATE TABLE plano_assinatura_tb (
    id_plano INT PRIMARY KEY AUTO_INCREMENT,
    tipo_plano ENUM('basico', 'premium', 'familia') NOT NULL,
    preco_mensal DECIMAL(10,2) NOT NULL,
    quantidade_max_perfis INT NOT NULL,
    qualidade_musica ENUM('baixa', 'media', 'alta', 'hi-fi') NOT NULL,
    quantidade_anuncios ENUM('nenhum', 'reduzido', 'normal') NOT NULL
);


CREATE TABLE usuario_tb (
    id_usuarios INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(45) NOT NULL,
    email VARCHAR(49) NOT NULL UNIQUE,
    data_nascimento DATE NOT NULL,
    status ENUM('ativo', 'inativo', 'suspenso') DEFAULT 'ativo',
    data_cadastro DATE NOT NULL,
    pais VARCHAR(45) NOT NULL,
    estado VARCHAR(45),
    genero VARCHAR(45),
    plano_assinatura_tb_id_plano INT,
    senha VARCHAR(256) NOT NULL,
    FOREIGN KEY (plano_assinatura_tb_id_plano) REFERENCES plano_assinatura_tb(id_plano)
);


CREATE TABLE perfil_usuario_tb (
    id_perfil INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    usuario_tb_id_usuarios INT NOT NULL,
    FOREIGN KEY (usuario_tb_id_usuarios) REFERENCES usuario_tb(id_usuarios)
);


CREATE TABLE artista_tb (
    id_artista INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    nacionalidade VARCHAR(100),
    data_nascimento DATE
);

CREATE TABLE album_tb (
    id_album INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    data_lancamento DATE NOT NULL,
    artista_tb_id_artista INT NOT NULL,
    FOREIGN KEY (artista_tb_id_artista) REFERENCES artista_tb(id_artista)
);


CREATE TABLE conteudo_tb (
    id_conteudo INT PRIMARY KEY AUTO_INCREMENT,
    titulo VARCHAR(100) NOT NULL,
    categoria ENUM('musica', 'podcast', 'video') NOT NULL,
    classificacao VARCHAR(10),
    duracao TIME NOT NULL,
    artista_tb_id_artista INT,
    tipo_conteudo ENUM('single', 'episodio', 'album', 'serie'),
    FOREIGN KEY (artista_tb_id_artista) REFERENCES artista_tb(id_artista)
);


CREATE TABLE musica_tb (
    id_musica INT PRIMARY KEY AUTO_INCREMENT,
    conteudo_tb_id_conteudo INT NOT NULL,
    album_tb_id_album INT,
    FOREIGN KEY (conteudo_tb_id_conteudo) REFERENCES conteudo_tb(id_conteudo),
    FOREIGN KEY (album_tb_id_album) REFERENCES album_tb(id_album)
);


CREATE TABLE podcast_tb (
    id_podcast INT PRIMARY KEY AUTO_INCREMENT,
    conteudo_tb_id_conteudo INT NOT NULL,
    apresentador VARCHAR(100) NOT NULL,
    numero_episodio VARCHAR(20),
    FOREIGN KEY (conteudo_tb_id_conteudo) REFERENCES conteudo_tb(id_conteudo)
);


CREATE TABLE playlist_tb (
    id_playlist INT PRIMARY KEY AUTO_INCREMENT,
    nome_playlist VARCHAR(100) NOT NULL,
    data_criacao DATE NOT NULL,
    perfil_usuario_tb_id_perfil INT NOT NULL,
    FOREIGN KEY (perfil_usuario_tb_id_perfil) REFERENCES perfil_usuario_tb(id_perfil)
);


CREATE TABLE playlist_conteudo_tb (
    playlist_tb_id_playlist INT NOT NULL,
    conteudo_tb_id_conteudo INT NOT NULL,
    ordem INT,
    data_adicao DATE NOT NULL,
    PRIMARY KEY (playlist_tb_id_playlist, conteudo_tb_id_conteudo),
    FOREIGN KEY (playlist_tb_id_playlist) REFERENCES playlist_tb(id_playlist),
    FOREIGN KEY (conteudo_tb_id_conteudo) REFERENCES conteudo_tb(id_conteudo)
);


CREATE TABLE biblioteca_tb (
    id_biblioteca INT PRIMARY KEY AUTO_INCREMENT,
    perfil_usuario_tb_id_perfil INT NOT NULL,
    tipo ENUM('favoritos', 'downloads', 'mais_tocados') NOT NULL,
    data_adicao DATE NOT NULL,
    conteudo_tb_id_conteudo INT NOT NULL,
    FOREIGN KEY (perfil_usuario_tb_id_perfil) REFERENCES perfil_usuario_tb(id_perfil),
    FOREIGN KEY (conteudo_tb_id_conteudo) REFERENCES conteudo_tb(id_conteudo)
);


CREATE TABLE avaliacao_tb (
    id_avaliacao INT PRIMARY KEY AUTO_INCREMENT,
    nota INT CHECK (nota BETWEEN 1 AND 5),
    comentario TEXT,
    data_avaliacao DATE NOT NULL,
    perfil_usuario_tb_id_perfil INT NOT NULL,
    conteudo_tb_id_conteudo INT NOT NULL,
    FOREIGN KEY (perfil_usuario_tb_id_perfil) REFERENCES perfil_usuario_tb(id_perfil),
    FOREIGN KEY (conteudo_tb_id_conteudo) REFERENCES conteudo_tb(id_conteudo)
);


CREATE TABLE historico_reproducao_tb (
    id_historico INT PRIMARY KEY AUTO_INCREMENT,
    data_reproducao DATETIME NOT NULL,
    duracao_reproduzida TIME NOT NULL,
    status ENUM('completo', 'parcial') NOT NULL,
    perfil_usuario_tb_id_perfil INT NOT NULL,
    conteudo_tb_id_conteudo INT NOT NULL,
    FOREIGN KEY (perfil_usuario_tb_id_perfil) REFERENCES perfil_usuario_tb(id_perfil),
    FOREIGN KEY (conteudo_tb_id_conteudo) REFERENCES conteudo_tb(id_conteudo)
);


CREATE TABLE pagamento_tb (
    id_pagamento INT PRIMARY KEY AUTO_INCREMENT,
    data_pagamento DATETIME NOT NULL,
    valor DECIMAL(10,2) NOT NULL,
    forma_pagamento ENUM('cartao', 'pix', 'boleto', 'paypal') NOT NULL,
    status_pagamento ENUM('pendente', 'concluido', 'falha', 'reembolsado') NOT NULL,
    usuario_tb_id_usuarios INT NOT NULL,
    FOREIGN KEY (usuario_tb_id_usuarios) REFERENCES usuario_tb(id_usuarios)
);

-- Consultas MSQL

SELECT * FROM usuario_tb;
SELECT * FROM playlist_tb;
SELECT * FROM perfil_usuario_tb;
SELECT * FROM album_tb;

SELECT table_name 
FROM information_schema.tables 
WHERE table_schema = 'spotifei' 
AND table_type = 'BASE TABLE';