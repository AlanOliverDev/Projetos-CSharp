-- Criação do banco de dados
CREATE DATABASE IF NOT EXISTS sga;
USE sga;

-- Tabela de Admins
CREATE TABLE admins (
    id INT AUTO_INCREMENT PRIMARY KEY,
    login VARCHAR(50) NOT NULL UNIQUE,
    senha VARCHAR(64) NOT NULL
);

-- Tabela de Alunos
CREATE TABLE alunos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    matricula VARCHAR(20) NOT NULL UNIQUE,
    nomeCompleto VARCHAR(100) NOT NULL,
    idade INT,
    cpf VARCHAR(20) UNIQUE,
    endereco VARCHAR(200)
);

-- consultas SQL 
SELECT * FROM admins;
SELECT * FROM alunos;
SELECT login , senha FROM admins;

SELECT id, matricula, nomeCompleto FROM alunos ORDER BY matricula ASC;
SELECT id, login FROM admins ORDER BY id ASC;

DESCRIBE alunos;
DESCRIBE admins;


