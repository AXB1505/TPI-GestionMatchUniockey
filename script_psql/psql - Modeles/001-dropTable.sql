/*
    @Author : Cyril Tobler
    
    La base de données clefBD doit contenir l'extension "uuid-ossp" 
    pour la génération des uuid avec la fonction uuid_generate_v4().

    CREATE EXTENSION "uuid-ossp";

    select uuid_generate_v4();
*/
-- Suppression de tables


DROP TABLE IF EXISTS historique_utilisation CASCADE
;

DROP TABLE IF EXISTS groupes_groupes CASCADE
;

DROP TABLE IF EXISTS acces CASCADE
;

DROP TABLE IF EXISTS personnes CASCADE
;

DROP TABLE IF EXISTS clefs CASCADE
;

DROP TABLE IF EXISTS clefs_personnes CASCADE
;

DROP TABLE IF EXISTS groupes CASCADE
;

DROP TABLE IF EXISTS serrures CASCADE
;

DROP TABLE IF EXISTS lieux CASCADE
;


