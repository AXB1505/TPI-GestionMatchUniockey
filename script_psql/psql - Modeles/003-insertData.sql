/*
    @Author : Darwin Ameli
    
    Clefs - Script d'insertion des données de demonstration

    La base de données clefDB doit contenir l'extension "uuid-ossp" 
    pour la génération des uuid avec la fonction uuid_generate_v4().

*/

/*

       Création des extensions

*/

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

SELECT uuid_generate_v4(); /* Exemple d'uuid */

/*

       Insertion des données de groupes

*/

INSERT INTO groupes
       (uuid,
       nom,
       description)
VALUES(uuid_generate_v4(),
       'darwin.ameli@rpn.ch',
       'Groupe personnel'),
       (uuid_generate_v4(),
       'diogo.paivacosta@rpn.ch',
       'Groupe personnel'),
       (uuid_generate_v4(),
       'cyril.tobler@rpn.ch',
       'Groupe personnel'),
       (uuid_generate_v4(),
       'cedric.caillet@rpn.ch',
       'Groupe personnel'),
       (uuid_generate_v4(),
       'dominique.huguenin@rpn.ch',
       'Groupe personnel'),
       (uuid_generate_v4(),
       'Conciergerie',
       'Groupe contenant tout le personnel de conciergerie des deux sites'),
       (uuid_generate_v4(),
       'Conciergerie CIFOM',
       'Groupe contenant tout le personnel de conciergerie du CIFOM'),
       (uuid_generate_v4(),
       'Conciergerie CPLN',
       'Groupe contenant tout le personnel de conciergerie du CPLN'),
       (uuid_generate_v4(),
       'Enseignants',
       'Groupe contenant tous les enseignants des deux sites'),
       (uuid_generate_v4(),
       'Enseignants de prog',
       'Groupe contenant tous les enseignants de prog des deux sites'),
       (uuid_generate_v4(),
       'Direction',
       'Groupe contenant les membres de direction des deux sites'),
       (uuid_generate_v4(),
       'Direction CPLN',
       'Groupe contenant les membres de direction du CPLN'),
       (uuid_generate_v4(),
       'Direction CIFOM',
       'Groupe contenant les membres de direction du CIFOM'),
       (uuid_generate_v4(),
       'Etudiants',
       'Groupe contenant les etudiants des deux sites'),
       (uuid_generate_v4(),
       'Etudiants CIFOM',
       'Groupe contenant les etudiants du CIFOM'),
       (uuid_generate_v4(),
       'Etudiants CPLN',
       'Groupe contenant les etudiants du CPLN')
;


/*

       Insertion des données de personnes

*/

INSERT INTO personnes
       (uuid,
        nom, 
        prenom,
        email,
        groupes_uuid)
VALUES (uuid_generate_v4(),
        'Ameli',
        'Darwin',
        'darwin.ameli@rpn.ch',
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'darwin.ameli@rpn.ch'))
;


INSERT INTO personnes
       (uuid,
        nom, 
        prenom,
        email,
        groupes_uuid)
VALUES (uuid_generate_v4(),
        'Paiva Costa',
        'Diogo',
        'diogo.paivacosta@rpn.ch',
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'diogo.paivacosta@rpn.ch'))
;

INSERT INTO personnes
       (uuid,
        nom, 
        prenom,
        email,
        groupes_uuid)
VALUES (uuid_generate_v4(),
        'Tobler',
        'Cyril',
        'cyril.tobler@rpn.ch',
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'cyril.tobler@rpn.ch'))
;

INSERT INTO personnes
       (uuid,
        nom, 
        prenom,
        email,
        groupes_uuid)
VALUES (uuid_generate_v4(),
        'Caillet',
        'Cedric',
        'cedric.caillet@rpn.ch',
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'cedric.caillet@rpn.ch'))
;

INSERT INTO personnes
       (uuid,
        nom, 
        prenom,
        email,
        groupes_uuid)
VALUES (uuid_generate_v4(),
        'Huguenin',
        'Dominique',
        'dominique.huguenin@rpn.ch',
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'dominique.huguenin@rpn.ch'))
;


/*

       Insertion des données de clefs

*/

INSERT INTO clefs
       (numero_serie,
       status)
VALUES ('0000-0000-0000-0001',
        'ACTIVE') 
;

INSERT INTO clefs
       (numero_serie,
       status)
VALUES ('0000-0000-0000-0002',
        'ACTIVE')
;

INSERT INTO clefs
       (numero_serie,
       status)
VALUES ('0000-0000-0000-0003',
        'ACTIVE')
;

INSERT INTO clefs
       (numero_serie,
       status)
VALUES ('0000-0000-0000-0004',
        'ACTIVE')
;

INSERT INTO clefs
       (numero_serie,
       status)
VALUES ('0000-0000-0000-0005',
        'ACTIVE')
;


INSERT INTO clefs
       (numero_serie,
       status)
VALUES ('0000-0000-0000-0006',
        'DISFONCTIONNELLE'),
       ('0000-0000-0000-0007',
        'PERDUE'),
       ('0000-0000-0000-0008',
        'INACTIVE')
;


/*

       Insertion des données de clefs_personnes

*/

INSERT INTO clefs_personnes
       (personnes_uuid,
       clefs_numero_serie,
       date_debut)
VALUES ((SELECT uuid FROM personnes
        WHERE nom LIKE 'Caillet' AND 
        email LIKE 'cedric.caillet@rpn.ch' AND
        prenom LIKE 'Cedric'),
        '0000-0000-0000-0001',
        '2022-02-05')
;

INSERT INTO clefs_personnes
       (personnes_uuid,
       clefs_numero_serie,
       date_debut,
       date_fin)
VALUES ((SELECT uuid FROM personnes
        WHERE nom LIKE 'Tobler' AND 
        email LIKE 'cyril.tobler@rpn.ch' AND
        prenom LIKE 'Cyril'),
        '0000-0000-0000-0002',
        '2022-01-23',
        '2022-04-16')
;

INSERT INTO clefs_personnes
       (personnes_uuid,
       clefs_numero_serie,
       date_debut,
       date_fin)
VALUES ((SELECT uuid FROM personnes
        WHERE nom LIKE 'Paiva Costa' AND 
        email LIKE 'diogo.paivacosta@rpn.ch' AND
        prenom LIKE 'Diogo'),
        '0000-0000-0000-0003',
        '2022-02-05',
        '2022-04-12')
;

INSERT INTO clefs_personnes
       (personnes_uuid,
       clefs_numero_serie,
       date_debut)
VALUES ((SELECT uuid FROM personnes
        WHERE nom LIKE 'Huguenin' AND 
        email LIKE 'dominique.huguenin@rpn.ch' AND
        prenom LIKE 'Dominique'),
        '0000-0000-0000-0004',
        '2022-01-05')
;

INSERT INTO clefs_personnes
       (personnes_uuid,
       clefs_numero_serie,
       date_debut,
       date_fin)
VALUES ((SELECT uuid FROM personnes
        WHERE nom LIKE 'Ameli' AND 
        email LIKE 'darwin.ameli@rpn.ch' AND
        prenom LIKE 'Darwin'),
        '0000-0000-0000-0005',
        '2022-01-24',
        '2022-06-14')
;


/*

       Insertion des données de lieux

*/

/* CIFOM */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(), 
       'CIFOM',
       null) 
;

/* CIFOM - Etage 3 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(), 
       'Etage 3',
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'CIFOM')) /* Enfant de CIFOM */
;

/* CIFOM - Etage 3 - 315 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(),
       '315',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
       lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CIFOM'))) 
;
       
       /* ---- Changement de batiment ---- */
       
/* CPLN */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(),
       'CPLN',
       null) 
;

/* CPLN - Etage 1 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(), 
       'Etage 1',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'CPLN')) 
;

/* CPLN - Etage 1 - B101 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(),
       'B101',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
       lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))) /* Enfant de CPLN - Etage 1 */
;

/* CPLN - Etage 1 - B102 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(),
       'B102',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
       lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))) /* Enfant de CPLN - Etage 1 */

; 

/* CPLN - Etage 2 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(), 
       'Etage 2',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'CPLN')) /* Enfant de CPLN */
;

/* CPLN - Etage 2 - B205 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(),
       'B205',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 2' AND
       lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))) /* Enfant de CPLN - Etage 2 */
;

/* CPLN - Etage 2 - B206 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(),
       'B206',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 2' AND
       lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))) /* Enfant de CPLN - Etage 2 */
; 

/* CPLN - Etage 3 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(), 
       'Etage 3',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'CPLN')) /* Enfant de CPLN */
;

/* CPLN - Etage 3 - B302 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(),
       'B302',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
       lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))) /* Enfant de CPLN - Etage 3 */
;

/* CPLN - Etage 3 - B303 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(),
       'B303',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
       lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))) /* Enfant de CPLN - Etage 3 */
; 

/* CPLN - Etage 4 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(), 
       'Etage 4',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'CPLN')) /* Enfant de CPLN */
;

/* CPLN - Etage 4 - B401 */
INSERT INTO lieux (uuid, nom, lieu_parent_uuid)
VALUES (uuid_generate_v4(),
       'B401',
       (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 4' AND
       lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))) /* Enfant de CPLN - Etage 4 */
;


/*

       Insertion des données de serrures

*/

INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES (uuid_generate_v4(),
       'NORD',
       (SELECT uuid FROM lieux
        WHERE nom LIKE '315' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CIFOM')))) /* CIFOM - Etage 3 - 315 */
;

INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES (uuid_generate_v4(),
       'SUD',
       (SELECT uuid FROM lieux 
        WHERE nom LIKE '315' AND /* lieu à deux serrures */ 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CIFOM')))) /* CIFOM - Etage 3 - 315 */
;

INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES (uuid_generate_v4(),
       'EST',
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B101' AND  
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))) /* CPLN - Etage 1 - B101 */
;

INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES (uuid_generate_v4(),
       'EST',
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B102' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))) /* CPLN - Etage 1 - B102 */
;

INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES (uuid_generate_v4(),
       'NORD',
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B205' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 2' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))) /* CPLN - Etage 2 - B205 */
;

INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES (uuid_generate_v4(),
       'SUD',
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B206' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 2' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))) /* CPLN - Etage 2 - B206 */
;

INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES (uuid_generate_v4(),
       'EST',
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B302' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))) /* CPLN - Etage 3 - B302 */
;

INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES (uuid_generate_v4(),
       'NORD',
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B303' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))) /* CPLN - Etage 3 - B303 */
;

INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES (uuid_generate_v4(),
       'EST',
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B401' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 4' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))) /* CPLN - Etage 4 - B401 */
;


/*

       Insertion des données dans historique_utilisation

*/

INSERT INTO historique_utilisation
       (clefs_numero_serie,
        serrures_uuid,
        date_utilisation,
        status)
VALUES ('0000-0000-0000-0001', /* Clef de Cedric Caillet */ 
       (SELECT uuid FROM serrures WHERE cardinalite LIKE 'NORD' AND lieux_uuid = (SELECT uuid FROM lieux /* Serrure NORD */
        WHERE nom LIKE '315' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CIFOM')))), /* Adresse du CIFOM */
       '2022-03-01',
       true)
;

INSERT INTO historique_utilisation
       (clefs_numero_serie,
        serrures_uuid,
        date_utilisation,
        status)
VALUES ('0000-0000-0000-0001', /* Clef de Cedric Caillet */
       (SELECT uuid FROM serrures WHERE cardinalite LIKE 'SUD' AND lieux_uuid = (SELECT uuid FROM lieux /* Serrure SUD */
        WHERE nom LIKE '315' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CIFOM')))), /* Adresse du CIFOM */
       '2022-03-01',
       true)
;

/* Deux utilisations de la même clef (disctinction via la cardinalité) */
INSERT INTO historique_utilisation
       (clefs_numero_serie,
        serrures_uuid,
        date_utilisation,
        status)
VALUES ('0000-0000-0000-0004', /* Clef de Dominique Huguenin */
       (SELECT uuid FROM serrures WHERE cardinalite = 'NORD' AND lieux_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE '315' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CIFOM')))), /* Adresse du CIFOM */
       '2022-03-01',
       false),
       ('0000-0000-0000-0004', /* Clef de Dominique Huguenin */ 
       (SELECT uuid FROM serrures WHERE cardinalite = 'SUD' AND lieux_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE '315' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CIFOM')))), /* Adresse du CIFOM */
       '2022-03-01',
       false)
;

INSERT INTO historique_utilisation
       (clefs_numero_serie,
        serrures_uuid,
        date_utilisation,
        status)
VALUES ('0000-0000-0000-0003',  /* Clef de Diogo Paiva Costa */ 
       (SELECT uuid FROM serrures WHERE cardinalite = 'EST' AND lieux_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'B101' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))), /* Adresse du CPLN */
       '2022-03-02',
       true),
       ('0000-0000-0000-0002', /* Clef de Cyril Tobler */ 
       (SELECT uuid FROM serrures WHERE cardinalite = 'EST' AND lieux_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'B101' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))), /* Adresse du CPLN */
       '2022-03-02',
       true),
       ('0000-0000-0000-0005', /* Clef de Darwin Ameli */ 
       (SELECT uuid FROM serrures WHERE cardinalite = 'EST' AND lieux_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'B101' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))), /* Adresse du CPLN */
       '2022-03-02',
       true)
;

INSERT INTO historique_utilisation
       (clefs_numero_serie,
        serrures_uuid,
        date_utilisation,
        status)
VALUES ('0000-0000-0000-0003',
       (SELECT uuid FROM serrures WHERE cardinalite = 'EST' AND lieux_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'B102' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))), /* Adresse du CPLN */
       '2022-03-03',
       true),
       ('0000-0000-0000-0005',
       (SELECT uuid FROM serrures WHERE cardinalite = 'EST' AND lieux_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'B102' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))), /* Adresse du CPLN */
       '2022-03-03',
       true)
;

INSERT INTO historique_utilisation
       (clefs_numero_serie,
        serrures_uuid,
        date_utilisation,
        status)
VALUES ('0000-0000-0000-0002',
       (SELECT uuid FROM serrures WHERE cardinalite = 'NORD' AND lieux_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'B205' AND 
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 2' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN')))), /* Adresse du CPLN */
       '2022-03-04',
       true)
;


/*

       Insertion des données de groupes_groupes

*/

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants de prog'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Conciergerie CPLN'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Conciergerie'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Conciergerie CIFOM'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Conciergerie'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Direction CPLN'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Direction'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Direction CIFOM'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Direction'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants CIFOM'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants CPLN'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants'))
;


INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'darwin.ameli@rpn.ch'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants CIFOM'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'darwin.ameli@rpn.ch'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants CPLN'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'diogo.paivacosta@rpn.ch'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants CIFOM'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'cyril.tobler@rpn.ch'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants CIFOM'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'cedric.caillet@rpn.ch'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants de prog'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'dominique.huguenin@rpn.ch'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants de prog'))
;


/*

       Insertion des données de acces

*/

INSERT INTO acces
       (groupes_uuid,
        lieux_uuid,
        date_debut,
        date_fin)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants'),
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B101' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))), /* CPLN - Etage 1 - B101 */
        '2022-01-19',
        '2022-02-23') 
;

INSERT INTO acces
       (groupes_uuid,
        lieux_uuid,
        date_debut,
        date_fin)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants'),
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B102' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))), /* CPLN - Etage 1 - B102 */
        '2022-01-07',
        '2022-03-09') 
;

INSERT INTO acces
       (groupes_uuid,
        lieux_uuid,
        date_debut,
        date_fin)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants de prog'),
       (SELECT uuid FROM lieux 
        WHERE nom LIKE '315' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 3' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CIFOM'))), /* CIFOM - Etage 3 - 315 */
        '2022-02-01',
        '2022-05-25') 
;

INSERT INTO acces
       (groupes_uuid,
        lieux_uuid,
        date_debut,
        date_fin)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants'),
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B101' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))), /* CPLN - Etage 1 - B101 */
        '2022-03-01',
        '2022-03-12')
;

INSERT INTO acces
       (groupes_uuid,
        lieux_uuid,
        date_debut,
        date_fin)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Etudiants'),
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B102' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 1' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))), /* CPLN - Etage 1 - B101 */
        '2022-01-15',
        '2022-03-13')
;

INSERT INTO acces
       (groupes_uuid,
        lieux_uuid,
        date_debut,
        date_fin)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'cyril.tobler@rpn.ch'), /* Accès exclusif sur groupe perso */
       (SELECT uuid FROM lieux 
        WHERE nom LIKE 'B205' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux
        WHERE nom LIKE 'Etage 2' AND
        lieu_parent_uuid = (SELECT uuid FROM lieux WHERE nom LIKE 'CPLN'))), /* CPLN - Etage 2 - B205 */
        '2021-03-02',
        '2022-03-02')
;
