-- Gestion d'un club de football : crée la base `club_foot` avec des données d'exemple.
-- Comptes de démonstration : admin / demo1234 (administrateur), kmbappe / demo1234 (joueur).
-- Import : mysql -u root -p < database/club_foot.sql   (ou onglet Importer de phpMyAdmin)

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

DROP DATABASE IF EXISTS `club_foot`;
CREATE DATABASE `club_foot` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `club_foot`;

DROP TABLE IF EXISTS `appartient`;
CREATE TABLE `appartient` (
  `idJoueur` int NOT NULL,
  `idEq` int NOT NULL,
  PRIMARY KEY (`idJoueur`,`idEq`),
  KEY `idEq` (`idEq`),
  CONSTRAINT `appartient_ibfk_1` FOREIGN KEY (`idJoueur`) REFERENCES `joueur` (`idJoueur`) ON DELETE CASCADE,
  CONSTRAINT `appartient_ibfk_2` FOREIGN KEY (`idEq`) REFERENCES `equipe` (`idEq`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `appartient` VALUES (10,1),(11,1),(8,2),(9,2),(12,3),(13,3),(14,4),(15,4),(16,5),(17,5),(18,6),(19,6),(20,13),(21,13);

DROP TABLE IF EXISTS `equipe`;
CREATE TABLE `equipe` (
  `idEq` int NOT NULL AUTO_INCREMENT,
  `nomEq` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `positionEq` int DEFAULT NULL,
  `image` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`idEq`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `equipe` VALUES (1,'Paris Saint-Germain',1,'psg.png'),(2,'Real Madrid',2,'real.png'),(3,'Manchester City',3,'city.png'),(4,'Bayern Munich',5,'bayern.png'),(5,'Liverpool',7,'liverpool.png'),(6,'Juventus',6,'juventus.png'),(13,'Arsenal',4,'default.png');

DROP TABLE IF EXISTS `joueur`;
CREATE TABLE `joueur` (
  `idJoueur` int NOT NULL AUTO_INCREMENT,
  `nomJoueur` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `prenomJoueur` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `numJoueur` int NOT NULL,
  `posteJoueur` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`idJoueur`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `joueur` VALUES (8,'Mbappe','Kylian',7,'Attaquant'),(9,'Courtois','Thibaut',1,'Gardien'),(10,'Dembélé','Ousmane',10,'Attaquant'),(11,'Hakimi','Achraf',2,'Défenseur'),(12,'Haaland','Erling',9,'Attaquant'),(13,'De Bruyne','Kevin',8,'Milieu'),(14,'Neuer','Manuel',1,'Gardien'),(15,'Kane','Harry',9,'Attaquant'),(16,'Szoboszlai','Dominik',8,'Milieu'),(17,'Salah','Mohamed',11,'Attaquant'),(18,'Kolo Muani','Randal',20,'Attaquant'),(19,'Thuram','Khéphren',19,'Milieu'),(20,'Rice','Declan',8,'Millieu'),(21,'Merino','Mikel',6,'Millieu');

DROP TABLE IF EXISTS `lieu`;
CREATE TABLE `lieu` (
  `idLieu` int NOT NULL AUTO_INCREMENT,
  `nomLieu` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`idLieu`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `lieu` VALUES (1,'Parc des Princes'),(2,'Santiago Bernabéu'),(3,'Etihad Stadium'),(4,'Allianz Arena'),(5,'Anfield'),(6,'Allianz Stadium');

DROP TABLE IF EXISTS `participe`;
CREATE TABLE `participe` (
  `idRencontre` int NOT NULL,
  `idEq` int NOT NULL,
  `score` int DEFAULT NULL,
  PRIMARY KEY (`idRencontre`,`idEq`),
  KEY `idEq` (`idEq`),
  CONSTRAINT `participe_ibfk_1` FOREIGN KEY (`idRencontre`) REFERENCES `rencontre` (`idRencontre`) ON DELETE CASCADE,
  CONSTRAINT `participe_ibfk_2` FOREIGN KEY (`idEq`) REFERENCES `equipe` (`idEq`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `participe` VALUES (2,1,3),(2,2,0),(3,3,2),(3,4,1),(4,5,0),(4,6,0),(5,4,3),(5,6,2),(6,1,2),(6,5,2),(7,1,NULL),(7,4,NULL),(8,2,NULL),(8,3,NULL),(10,1,0),(10,13,0);

DROP TABLE IF EXISTS `rencontre`;
CREATE TABLE `rencontre` (
  `idRencontre` int NOT NULL AUTO_INCREMENT,
  `dateRencontre` datetime NOT NULL,
  `idLieu` int DEFAULT NULL,
  PRIMARY KEY (`idRencontre`),
  KEY `idLieu` (`idLieu`),
  CONSTRAINT `rencontre_ibfk_1` FOREIGN KEY (`idLieu`) REFERENCES `lieu` (`idLieu`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `rencontre` VALUES (2,'2024-02-10 21:00:00',1),(3,'2024-02-15 18:30:00',2),(4,'2024-02-20 21:00:00',3),(5,'2024-03-05 00:00:00',4),(6,'2024-03-10 21:00:00',5),(7,'2025-05-23 20:45:00',1),(8,'2025-05-30 19:30:00',2),(10,'2025-04-29 21:00:00',NULL);

DROP TABLE IF EXISTS `utilisateur`;
CREATE TABLE `utilisateur` (
  `idUt` int NOT NULL AUTO_INCREMENT,
  `login` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `mdp` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `typeCompte` enum('admin','joueur') COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`idUt`),
  UNIQUE KEY `login` (`login`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `utilisateur` VALUES (1,'admin','100000.ESybcAk2eA1JRdxQ+XcZ7A==.iKE3gQdtLg+mz78mBfUYbbo0Bvt9X752zEdXgPUe8sg=','admin'),(3,'kmbappe','100000.bfjzX8Q39j/nyhbL+qNJDw==.xonW+r7IHgyVEbqXzdPHIJUsinrqxJsJjjzR92ENGV8=','joueur'),(4,'kbenzema','100000.X8C6u8j6rkm4TSuvJRNCuQ==.t7pj39BWEHltnLaE6rTVhOZNPweZvKny4Ll/V3DVZss=','joueur'),(5,'kdebruyne','100000.AsxvJAnZU9/c7e/C0RK2yA==.fG5n16huktBK8CaorVymOSG4DENpUgGt10wTvbWxgys=','joueur'),(6,'mneuer','100000.h0+yrYT7wIHlZHW4FgLtXQ==.Gr7wJ4Y8Ro8qJ6evUOy+g7Oz7HHdyP0QuwjebSCsSiw=','joueur'),(7,'msalah','100000.nvxYrwKNNsrZMLtB/yVRLA==.eU0f/awZGTLXpvPcEbF6NBMceJNQHfb34Bpnwu9c/S0=','joueur'),(8,'cronaldo','100000.FSCFPePBlynxSS9lX3cbmQ==.+J99bC3sWIGYqy2QeajYP92GSzdXKLDmB5x+ua5KvvY=','joueur');

SET FOREIGN_KEY_CHECKS = 1;
