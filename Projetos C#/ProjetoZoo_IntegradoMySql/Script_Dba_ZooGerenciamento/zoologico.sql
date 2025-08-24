CREATE DATABASE zoologico CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE zoologico;

CREATE TABLE `animais` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `idade` int NOT NULL,
  `especie` varchar(100) NOT NULL,
  `tipo` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE mamiferos (
    animal_id INT PRIMARY KEY,
    tem_pelo BOOLEAN NOT NULL,
    habitat VARCHAR(100) NOT NULL,
    FOREIGN KEY (animal_id) REFERENCES animais(id) ON DELETE CASCADE
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE repteis (
    animal_id INT PRIMARY KEY,
    tem_escamas BOOLEAN NOT NULL,
    temperatura_corporal VARCHAR(50) NOT NULL,
    FOREIGN KEY (animal_id) REFERENCES animais(id) ON DELETE CASCADE
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

CREATE TABLE aves (
    animal_id INT PRIMARY KEY,
    pode_voar BOOLEAN NOT NULL,
    tamanho_asas DOUBLE NOT NULL,
    FOREIGN KEY (animal_id) REFERENCES animais(id) ON DELETE CASCADE
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

SHOW VARIABLES LIKE 'character_set%';
SHOW VARIABLES LIKE 'collation%';
SHOW CREATE TABLE animais;

INSERT INTO animais (nome, idade, especie, tipo) VALUES ('Leão', 7, 'Pantera leão', 'Mamifero');
INSERT INTO mamiferos (animal_id, tem_pelo, habitat) VALUES (LAST_INSERT_ID(), TRUE, 'Savana');
SELECT id, nome, especie FROM animais;