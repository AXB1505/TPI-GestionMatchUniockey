/*
    @Author : Alessandro Borrani
    Code sql pour la création des tables de la BD
*/
-- Création de tables

CREATE TABLE IF NOT EXISTS Categorie(
    num NUMERIC NOT NULL,                                     -- Clé primaire identifiant de la catégorie
    nom TEXT NOT NULL,                                        -- Nom de l'équipe || Unique (NID-1)
    dureeMatch INTERVAL NOT NULL,                                 -- Durée d'une periode d'un match pour la catégorie
    nombrePeriode NUMERIC NOT NULL,                           -- Nombre de période d'un match pour la catégorie
    
    CONSTRAINT pk_categorie
        PRIMARY KEY (num)
)   
;

CREATE TABLE IF NOT EXISTS Equipe(
    num NUMERIC NOT NULL,                                     -- Clé primaire identifiant de l'équipe                              -- Clé étrangère identifiant du lieu de l'équipe
    Cat_num  NUMERIC NOT NULL,                                -- Clé étrangère identifiant de la catégorie de l'équipe || Unique avec nom (NID-1)
    nom TEXT NOT NULL,                                        -- Nom de l'équipe || Unique (NID-1) avec Cat_num
    
    CONSTRAINT pk_equipe
        PRIMARY KEY (num)
)   
;

CREATE TABLE IF NOT EXISTS Match(
    num NUMERIC NOT NULL,                                     -- Clé primaire identifiant du résultat
    Equ_num1 NUMERIC NOT NULL,                                -- Clé étrangère identifiant de l'équipe 1 || Unique avec Equ_num2 et debutMatch (NID-1)
    Equ_num2  NUMERIC NOT NULL,                               -- Clé étrangère identifiant de l'équipe 2 || Unique avec Equ_num1 et debutMatch (NID-1)
    debutMatch TIMESTAMP NOT NULL,                            -- Date et heure de début du match || Unique avec Equ_num1 et Equ_num2 (NID-1)
    scoreEquipe1 NUMERIC NOT NULL,                            -- Score de l'équipe 1
    scoreEquipe2 NUMERIC NOT NULL,                            -- Score de l'équipe 2

    CONSTRAINT pk_match
        PRIMARY KEY (num)
)   
;



-- Mise en place des contraintes

ALTER TABLE IF EXISTS Categorie
    DROP CONSTRAINT IF EXISTS NID1_Cat_Num,
    ADD CONSTRAINT NID1_Cat_Num
            UNIQUE (nom)
;

ALTER TABLE IF EXISTS Equipe
    DROP CONSTRAINT IF EXISTS FK2_Cat,
    ADD CONSTRAINT FK2_Cat
            FOREIGN KEY (Cat_num)
            REFERENCES Categorie(num),
    DROP CONSTRAINT IF EXISTS NID1_Equ_Cat_nom,
    ADD CONSTRAINT NID1_Equ_Cat_nom
            UNIQUE (nom, Cat_num)
;

ALTER TABLE IF EXISTS Match
    DROP CONSTRAINT IF EXISTS FK1_Equ1,
    ADD CONSTRAINT FK1_Equ1
            FOREIGN KEY (Equ_num1)
            REFERENCES Equipe(num),
    DROP CONSTRAINT IF EXISTS FK2_Equ2,
    ADD CONSTRAINT FK2_Equ2
            FOREIGN KEY (Equ_num2)
            REFERENCES Equipe(num),
    DROP CONSTRAINT IF EXISTS NID1_Mat_Equ1_Equ2_debutMatch,
    ADD CONSTRAINT NID1_Mat_Equ1_Equ2_debutMatch
            UNIQUE (Equ_num1, Equ_num2, debutMatch),
    DROP CONSTRAINT IF EXISTS CHK_Match_Equipes,
    ADD CONSTRAINT CHK_Match_Equipes
            CHECK (Equ_num1 != Equ_num2)
;