/*
    @Author : Alessandro Borrani
    Code sql pour la suppression de toutes les tables de la BD
*/
-- Suppression de tables

DROP TABLE IF EXISTS Categorie CASCADE                          -- Suppression de la table Categorie en cascade
;

DROP TABLE IF EXISTS Equipe CASCADE                             -- Suppression de la table Equipe en cascade
;

DROP TABLE IF EXISTS Match CASCADE                              -- Suppression de la table Match en cascade
;

/*
    @Author : Alessandro Borrani
    Code sql pour la création des tables de la BD
*/
-- Création de tables

CREATE TABLE IF NOT EXISTS Categorie(
    num SERIAL PRIMARY KEY,                                     -- Clé primaire identifiant de la catégorie
    nom TEXT NOT NULL                                           -- Nom de l'équipe || Unique (NID-1)
)   
;

CREATE TABLE IF NOT EXISTS Equipe(
    num SERIAL PRIMARY KEY,                                     -- Clé primaire identifiant de l'équipe                              -- Clé étrangère identifiant du lieu de l'équipe
    Cat_num  INTEGER NOT NULL,                                  -- Clé étrangère identifiant de la catégorie de l'équipe || Unique avec nom (NID-1)
    nom TEXT NOT NULL                                           -- Nom de l'équipe || Unique (NID-1) avec Cat_num
)   
;

CREATE TABLE IF NOT EXISTS Match(
    num SERIAL PRIMARY KEY,                                     -- Clé primaire identifiant du résultat
    Equ_num1 INTEGER NOT NULL,                                  -- Clé étrangère identifiant de l'équipe 1 || Unique avec Equ_num2 et debutMatch (NID-1)
    Equ_num2  INTEGER NOT NULL,                                 -- Clé étrangère identifiant de l'équipe 2 || Unique avec Equ_num1 et debutMatch (NID-1)
    debutMatch TIMESTAMP NOT NULL,                              -- Date et heure de début du match || Unique avec Equ_num1 et Equ_num2 (NID-1)
    scoreEquipe1 NUMERIC NOT NULL,                              -- Score de l'équipe 1
    scoreEquipe2 NUMERIC NOT NULL                               -- Score de l'équipe 2
)   
;



-- Mise en place des contraintes

ALTER TABLE IF EXISTS Categorie                                 -- Modification de la table Categorie si elle existe
    DROP CONSTRAINT IF EXISTS NID1_Cat_Num,                     -- Vérification que NID1_Cat_Num n'existe pas déjà et suppression si c'est le cas
    ADD CONSTRAINT NID1_Cat_Num                                 -- Ajout d'une contrainte de nom NID1_Cat_Num
            UNIQUE (nom)                                        -- Unicité du champ nom de la table Categorie
;

ALTER TABLE IF EXISTS Equipe                                    -- Modification de la table Equipe si elle existe
    DROP CONSTRAINT IF EXISTS FK2_Cat,                          -- Vérification que FK2_Cat n'existe pas déjà et suppression si c'est le cas
    ADD CONSTRAINT FK2_Cat                                      -- Ajout d'une contrainte de nom FK2_Cat
            FOREIGN KEY (Cat_num)                               -- Définition de la clé étrangère Cat_num
            REFERENCES Categorie(num),                          -- Référence de la clé étrangère sur la clé primaire de la table Categorie
    DROP CONSTRAINT IF EXISTS NID1_Equ_Cat_nom,                 -- Vérification que NID1_Equ_Cat_nom n'existe pas déjà et suppresion si c'est le cas
    ADD CONSTRAINT NID1_Equ_Cat_nom                             -- Ajout d'une contrainte de nom NID1_Equ_Cat_Nom
            UNIQUE (nom, Cat_num)                               -- Unicité de la paire de champ nom et Cat_num de la table Equipe
;

ALTER TABLE IF EXISTS Match                                     -- Modification de la table Match si elle existe
    DROP CONSTRAINT IF EXISTS FK1_Equ1,                         -- Vérification que FK1_Equ1 n'existe pas déjà et suppression si c'est le cas
    ADD CONSTRAINT FK1_Equ1                                     -- Ajout d'une contrainte de nom FK1_Equ1 
            FOREIGN KEY (Equ_num1)                              -- Définition de la clé étrangère Equ_num1
            REFERENCES Equipe(num)                              -- Référence de la clé étrangère sur la clé primaire de la table Equipe
    DROP CONSTRAINT IF EXISTS FK2_Equ2,                         -- Vérification que FK2_Equ2 n'existe pas déjà et suppression si c'est le cas
    ADD CONSTRAINT FK2_Equ2                                     -- Ajout d'une contrainte de nom FK2_Equ2 
            FOREIGN KEY (Equ_num2)                              -- Définition de la clé étrangère Equ_num2
            REFERENCES Equipe(num),                             -- Référence de la clé étrangère sur la clé primaire de la table Equipe
    DROP CONSTRAINT IF EXISTS NID1_Mat_Equ1_Equ2_debutMatch,    -- Vérification que NID1_Mat_Equ1_Equ2_debutMatch n'existe pas déjà et suppression si c'est le cas
    ADD CONSTRAINT NID1_Mat_Equ1_Equ2_debutMatch,               -- Ajout d'une contrainte de nom NID1_Mat_Equ1_Equ2_debutMatch 
            UNIQUE (Equ_num1, Equ_num2, debutMatch),            -- Unicité des champs Equ_num1, Equ_num2, debutMatch
    DROP CONSTRAINT IF EXISTS CHK_Match_Equipes,                -- Vérification que CHK_Match_Equipes n'existe pas déjà et suppression si c'est le cas
    ADD CONSTRAINT CHK_Match_Equipes                            -- Ajout d'une contrainte de nom CHK_Match_Equipes
            CHECK (Equ_num1 != Equ_num2)                        -- Check que l'équipe1 ne soit pas égale à l'équipe2
;
