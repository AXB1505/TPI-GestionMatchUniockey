/*
    @Author : Cyril Tobler

    La base de données clefDB doit contenir l'extension "uuid-ossp" 
    pour la génération des uuid avec la fonction uuid_generate_v4().

    CREATE EXTENSION "uuid-ossp";

    select uuid_generate_v4();
    
    Cette version à des modification de la table goupes_lieux 
    qui à une date de début d'accès et de fin d'accès
    qui ne sont pas modifiable donc pour redonner l'accès à un groupe,
    il faudrait recréer un tuples avec de nouvelles donnée de ce fais il y aurait un 
    historiques des acès avec des dates  
*/
-- Création de tables
CREATE TABLE IF NOT EXISTS historique_utilisation(
    clefs_numero_serie TEXT NOT NULL,                         -- PFK
    serrures_uuid UUID NOT NULL,                              -- PFK
    date_utilisation TIMESTAMP,
    status BOOLEAN NOT NULL,
    instant_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     --Champs d'audit remplie automatiquement
    user_creation TEXT DEFAULT current_user,                  --Champs d'audit remplie automatiquement
    instant_modification TIMESTAMP DEFAULT CURRENT_TIMESTAMP, --Champs d'audit remplie automatiquement
    user_modification TEXT DEFAULT NULL,                      --Champs d'audit remplie automatiquement
    
    CONSTRAINT pk_historique_utilisation
        PRIMARY KEY (clefs_numero_serie, serrures_uuid, date_utilisation)
)   
;

CREATE TABLE IF NOT EXISTS groupes(
    uuid UUID NOT NULL,                                       -- PK AID
    nom TEXT NOT NULL,                                        -- Unique
    description TEXT,
    instant_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     --Champs d'audit remplie automatiquement
    user_creation TEXT DEFAULT current_user,                  --Champs d'audit remplie automatiquement
    instant_modification TIMESTAMP, --Champs d'audit remplie automatiquement
    user_modification TEXT DEFAULT NULL,                      --Champs d'audit remplie automatiquement

    CONSTRAINT pk_groupes
        PRIMARY KEY (uuid)
)   
;

CREATE TABLE IF NOT EXISTS groupes_groupes(
    enfant_uuid UUID NOT NULL,                                --PFK groupe enfant
    parent_uuid UUID NOT NULL,                                --PFK groupe parent
    instant_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     --Champs d'audit remplie automatiquement
    user_creation TEXT DEFAULT current_user,                  --Champs d'audit remplie automatiquement
    instant_modification TIMESTAMP DEFAULT CURRENT_TIMESTAMP, --Champs d'audit remplie automatiquement
    user_modification TEXT DEFAULT NULL,                      --Champs d'audit remplie automatiquement

    CONSTRAINT pk_groupes_groupes
        PRIMARY KEY (enfant_uuid, parent_uuid)
)   
;

CREATE TABLE IF NOT EXISTS acces(
    groupes_uuid UUID NOT NULL,                               -- PFK
    lieux_uuid UUID NOT NULL,                                 -- PFK
    date_debut TIMESTAMP NOT NULL,                                 --Non Modifiable
    date_fin TIMESTAMP NOT NULL,                                   --Non Modifiable
    instant_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     --Champs d'audit remplie automatiquement
    user_creation TEXT DEFAULT current_user,                  --Champs d'audit remplie automatiquement
    instant_modification TIMESTAMP DEFAULT CURRENT_TIMESTAMP, --Champs d'audit remplie automatiquement
    user_modification TEXT DEFAULT NULL,                      --Champs d'audit remplie automatiquement

    CONSTRAINT pk_acces
        PRIMARY KEY (groupes_uuid, lieux_uuid)
)   
;

CREATE TABLE IF NOT EXISTS lieux(
    uuid UUID NOT NULL,                                       --PK AID
    nom TEXT NOT NULL,
    lieu_parent_uuid UUID,                  --FK                                   
    instant_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     --Champs d'audit remplie automatiquement
    user_creation TEXT DEFAULT current_user,                  --Champs d'audit remplie automatiquement
    instant_modification TIMESTAMP DEFAULT CURRENT_TIMESTAMP, --Champs d'audit remplie automatiquement
    user_modification TEXT DEFAULT NULL,                      --Champs d'audit remplie automatiquement

    CONSTRAINT pk_lieux
        PRIMARY KEY (uuid)
)   
;

CREATE TABLE IF NOT EXISTS serrures(
    uuid UUID NOT NULL,                                       --PK AID
    cardinalite TEXT,
    lieux_uuid UUID,                                          --FK
    instant_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     --Champs d'audit remplie automatiquement
    user_creation TEXT DEFAULT current_user,                  --Champs d'audit remplie automatiquement
    instant_modification TIMESTAMP DEFAULT CURRENT_TIMESTAMP, --Champs d'audit remplie automatiquement
    user_modification TEXT DEFAULT NULL,                      --Champs d'audit remplie automatiquement

    CONSTRAINT pk_serrures
        PRIMARY KEY (uuid)
)   
;

CREATE TABLE IF NOT EXISTS clefs(
    numero_serie TEXT NOT NULL,                               --PK NID
    status TEXT,
    instant_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     --Champs d'audit remplie automatiquement
    user_creation TEXT DEFAULT current_user,                  --Champs d'audit remplie automatiquement
    instant_modification TIMESTAMP DEFAULT CURRENT_TIMESTAMP, --Champs d'audit remplie automatiquement
    user_modification TEXT DEFAULT NULL,                      --Champs d'audit remplie automatiquement

    CONSTRAINT pk_clefs
        PRIMARY KEY (numero_serie)
)   
;

CREATE TABLE IF NOT EXISTS personnes(
    uuid UUID NOT NULL,                                     --PK AID
    nom TEXT NOT NULL,                                      --NID
    prenom TEXT NOT NULL,                                   --NID
    email TEXT,
    groupes_uuid UUID,
    instant_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     --Champs d'audit remplie automatiquement
    user_creation TEXT DEFAULT current_user,                  --Champs d'audit remplie automatiquement
    instant_modification TIMESTAMP DEFAULT CURRENT_TIMESTAMP, --Champs d'audit remplie automatiquement
    user_modification TEXT DEFAULT NULL,                      --Champs d'audit remplie automatiquement

    CONSTRAINT pk_personnes
        PRIMARY KEY (uuid)
)   
;

CREATE TABLE IF NOT EXISTS clefs_personnes(
    personnes_uuid UUID NOT NULL,
    clefs_numero_serie TEXT NOT NULL,
    date_debut TIMESTAMP NOT NULL,
    date_fin TIMESTAMP,
    instant_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     --Champs d'audit remplie automatiquement
    user_creation TEXT DEFAULT current_user,                  --Champs d'audit remplie automatiquement
    instant_modification TIMESTAMP DEFAULT CURRENT_TIMESTAMP, --Champs d'audit remplie automatiquement
    user_modification TEXT DEFAULT NULL,                      --Champs d'audit remplie automatiquement

     CONSTRAINT pk_clefs_personnes
        PRIMARY KEY (personnes_uuid,clefs_numero_serie)
)
;

-- Mise en place des contraintes
ALTER TABLE IF EXISTS historique_utilisation
    DROP CONSTRAINT IF EXISTS FK1_historique_utilisation_clefs,
    ADD CONSTRAINT FK1_historique_utilisation_clefs
            FOREIGN KEY (clefs_numero_serie)
            REFERENCES clefs (numero_serie) ON DELETE CASCADE,
     DROP CONSTRAINT IF EXISTS FK2_historique_utilisation_clefs,
    ADD CONSTRAINT FK2_historique_utilisation_serrures
            FOREIGN KEY (serrures_uuid)
            REFERENCES serrures (uuid) ON DELETE CASCADE
;

ALTER TABLE IF EXISTS groupes
    DROP CONSTRAINT IF EXISTS U1_groupes_nom,
    ADD CONSTRAINT U1_groupes_nom
            UNIQUE (nom)
;

ALTER TABLE IF EXISTS groupes_groupes
    DROP CONSTRAINT IF EXISTS FK1_groupes_groupes_enfant,
    ADD CONSTRAINT FK1_groupes_groupes_enfant
            FOREIGN KEY (enfant_uuid)
            REFERENCES groupes (uuid) ON DELETE CASCADE,
     DROP CONSTRAINT IF EXISTS FK2_groupes_groupes_parent,
    ADD CONSTRAINT FK2_groupes_groupes_parent
            FOREIGN KEY (parent_uuid)
            REFERENCES groupes (uuid) ON DELETE CASCADE
;

ALTER TABLE IF EXISTS groupes_personnes
    DROP CONSTRAINT IF EXISTS FK1_groupes_personnes_groupes,
    ADD CONSTRAINT FK1_groupes_personnes_groupes
            FOREIGN KEY (groupes_uuid)
            REFERENCES groupes (uuid) ON DELETE CASCADE,
     DROP CONSTRAINT IF EXISTS FK2_groupes_personnes_personnes,
    ADD CONSTRAINT FK2_groupes_personnes_personnes
            FOREIGN KEY (personnes_uuid)
            REFERENCES personnes (uuid) ON DELETE CASCADE
;

ALTER TABLE IF EXISTS acces
    DROP CONSTRAINT IF EXISTS FK1_acces_groupes,
    ADD CONSTRAINT FK1_acces_groupes
            FOREIGN KEY (groupes_uuid)
            REFERENCES groupes (uuid) ON DELETE CASCADE,
     DROP CONSTRAINT IF EXISTS FK2_acces_lieux,
    ADD CONSTRAINT FK2_acces_lieux
            FOREIGN KEY (lieux_uuid)
            REFERENCES lieux (uuid) ON DELETE CASCADE 
;

ALTER TABLE IF EXISTS lieux
    DROP CONSTRAINT IF EXISTS FK1_lieux_lieu_parent,
    ADD CONSTRAINT FK1_lieux_lieu_parent
            FOREIGN KEY (lieu_parent_uuid)
            REFERENCES lieux (uuid) ON DELETE CASCADE
;

ALTER TABLE IF EXISTS serrures
    DROP CONSTRAINT IF EXISTS FK1_serrures_lieux,
    ADD CONSTRAINT FK1_serrures_lieux
            FOREIGN KEY (lieux_uuid)
            REFERENCES lieux (uuid) ON DELETE CASCADE,
     DROP CONSTRAINT IF EXISTS CK1_serrures_cardinalite,
    ADD CONSTRAINT CK2_serrures_cardinalite
            CHECK(cardinalite IN ('NORD','EST','SUD','OUEST'))
;

ALTER TABLE IF EXISTS clefs
     DROP CONSTRAINT IF EXISTS CK2_serrures_cardinalite,
    ADD CONSTRAINT CK1_clefs_status
            CHECK(status IN('ACTIVE','INACTIVE','PERDUE','DISFONCTIONNELLE'))
;

ALTER TABLE IF EXISTS clefs_personnes
    DROP CONSTRAINT IF EXISTS FK1_clefs_personnes_personnes,
    ADD CONSTRAINT FK1_clefs_personnes_personnes
        FOREIGN KEY (personnes_uuid)
        REFERENCES personnes (uuid) ON DELETE CASCADE,
    DROP CONSTRAINT IF EXISTS FK2_clefs_personnes_clefs,
    ADD CONSTRAINT FK2_clefs_personnes_clefs
        FOREIGN KEY (clefs_numero_serie)
        REFERENCES clefs (numero_serie) ON DELETE CASCADE
;

ALTER TABLE IF EXISTS personnes
    DROP CONSTRAINT IF EXISTS FK1_personnes_groupes,
    ADD CONSTRAINT FK1_personnes_groupes
            FOREIGN KEY (groupes_uuid)
            REFERENCES groupes (uuid) ON DELETE CASCADE,
    DROP CONSTRAINT IF EXISTS U1_personnes_nom_utilisateur_email_avs,
    ADD CONSTRAINT U1_personnes_email
            UNIQUE (email)
;

-- Mise en places de TRIGGERS et des procédures stockés

-- Fonction 
-- le := est similaire au = mais vu que les deux sont utilisée pour des 
-- notions diférentes les assignation se font avec := et les tests avec =
--
-- La fonction comme decrite si dessous remplace les champs donnée par l'utilisateur
-- de fais il ne peut pas modifier ou entrer des donnée de lui même 
CREATE OR REPLACE FUNCTION                              
F_maj_audit()                                           --Nom de la fonction
RETURNS TRIGGER AS $trigger$                            --Retour de la fonction
BEGIN                                   
  IF (tg_op = 'INSERT') THEN                            --Si l'opération est un INSERT alors
    NEW.instant_creation := current_timestamp;              --instant_creation = now 
    NEW.user_creation := current_user;                      --user_creation = utilisateur actif
    NEW.instant_modification := NULL;                       --instant_modification = null
    NEW.user_modification := NULL;                          --user_modification = null
  ELSIF (tg_op = 'UPDATE') THEN                         --Si l'opération est un UPDATE
    NEW.instant_creation := OLD.instant_creation;           --instant_creation = valeur de instant_creation avant l'update 
    NEW.user_creation := OLD.user_creation;                 --user_creation = valeur de user_creation avant l'update
    NEW.instant_modification := current_timestamp;          --instant_modification = now 
    NEW.user_modification := current_user;                  --user_modification = utilisateur actif
  END IF ;                                              --Fin du IF
  RETURN NEW;                                           --Retourne la variable NEW pour l'INSERT ou UPDATE
END;                                                --Fin de la fonction
$trigger$ language 'plpgsql';

CREATE OR REPLACE FUNCTION                              
F_maj_historique()                                           --Nom de la fonction
RETURNS TRIGGER AS $trigger$                            --Retour de la fonction
BEGIN                                   
NEW.date_utilisation := OLD.date_utilisation;           --date_utilisation = valeur de date_utilisation avant l'update 
NEW.status := OLD.status;                                --status = valeur de status avant l'update
RETURN NEW;                                           --Retourne la variable NEW pour l'INSERT ou UPDATE
END;                                                --Fin de la fonction
$trigger$ language 'plpgsql';


CREATE OR REPLACE FUNCTION                              
F_maj_registre()                                           --Nom de la fonction
RETURNS TRIGGER AS $trigger$                               --Retour de la fonction
BEGIN                           --Si l'opération est un UPDATE
    NEW.date_debut := OLD.date_debut;                   --date_debut = valeur de date_debut avant l'update 
    NEW.date_fin  := OLD.date_fin ;                     --date_debut = valeur de date_debut avant l'update
                                            --Fin du IF
  RETURN NEW;                                           --Retourne la variable NEW pour l'INSERT ou UPDATE
END;   
$trigger$ language 'plpgsql';



--TRIGGERS


CREATE TRIGGER TR_BIUS_groupes
BEFORE INSERT OR UPDATE
ON groupes FOR EACH ROW
EXECUTE FUNCTION F_maj_audit()
;

CREATE TRIGGER TR_BIUS_groupes_groupes
BEFORE INSERT OR UPDATE
ON groupes FOR EACH ROW
EXECUTE FUNCTION F_maj_audit()
;


CREATE TRIGGER TR_BIUS_acces
BEFORE INSERT OR UPDATE
ON acces FOR EACH ROW
EXECUTE FUNCTION F_maj_audit()
;

CREATE TRIGGER TR_BUS_acces
BEFORE UPDATE
ON acces FOR EACH ROW
EXECUTE FUNCTION F_maj_registre()
;

CREATE TRIGGER TR_BIUS_lieux
BEFORE INSERT OR UPDATE
ON lieux FOR EACH ROW
EXECUTE FUNCTION F_maj_audit()
;

CREATE TRIGGER TR_BIUS_serrures
BEFORE INSERT OR UPDATE
ON serrures FOR EACH ROW
EXECUTE FUNCTION F_maj_audit()
;

CREATE TRIGGER TR_BIUS_clefs
BEFORE INSERT OR UPDATE
ON clefs FOR EACH ROW
EXECUTE FUNCTION F_maj_audit()
;

CREATE TRIGGER TR_BIUS_personnes
BEFORE INSERT OR UPDATE
ON personnes FOR EACH ROW
EXECUTE FUNCTION F_maj_audit()
;

CREATE TRIGGER TR_BIUS_clefs_personnes
BEFORE INSERT OR UPDATE
ON clefs_personnes FOR EACH ROW
EXECUTE FUNCTION F_maj_audit()
;


CREATE TRIGGER TR_BIUS_clefs_personnes_registre
BEFORE UPDATE
ON clefs_personnes FOR EACH ROW
EXECUTE FUNCTION F_maj_registre()
;

CREATE TRIGGER TR_BUS_historique_utilisation
BEFORE UPDATE
ON historique_utilisation FOR EACH ROW
EXECUTE FUNCTION F_maj_historique()
;

CREATE TRIGGER TR_BIUS_historique_utilisation
BEFORE INSERT OR UPDATE ON historique_utilisation 
FOR EACH ROW EXECUTE FUNCTION 
F_maj_audit();