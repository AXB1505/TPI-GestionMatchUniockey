/*
Afficher la liste des clefs :

  numero de serie   |      status      |       utilisateur         | mise en service | fin de service |
--------------------+------------------+---------------------------+-----------------+----------------+
0000-0000-0000-0001 | ACTIVE           | caillet.cedric@rpn.ch     | 2022-02-05      |                |
0000-0000-0000-0002 | ACTIVE           | cyril.tobler@rpn.ch       | 2022-01-23      | 2022-04-16     |
0000-0000-0000-0003 | ACTIVE           | diogo.paivacosta@rpn.ch   | 2022-02-05      | 2022-04-12     |
0000-0000-0000-0004 | ACTIVE           | dominique.huguenin@rpn.ch | 2022-01-05      |                |
0000-0000-0000-0005 | ACTIVE           | darwin.ameli@rpn.ch       | 2022-01-24      | 2022-06-14     |
0000-0000-0000-0006 | DISFONCTIONNELLE |                           | 2022-01-06      | 2022-03-14     |
0000-0000-0000-0007 | PERDUE           |                           | 2021-12-10      | 2022-04-20     |
0000-0000-0000-0008 | INACTIVE         |                           | 2021-06-11      | 2021-11-23     |
*/

SELECT c.numero_serie, c.status, p.email as utilisateur, cp.date_debut as "mise en service", cp.date_fin as "fin de service"
FROM clefs c
LEFT JOIN clefs_personnes cp on c.numero_serie = cp.clefs_numero_serie
LEFT JOIN personnes p on cp.personnes_uuid = p.uuid
;

/* Rechercher plein texte des champs */
-- Au moins un champ doit contenir le terme 'ACTIVE'.
SELECT c.numero_serie, c.status, p.email as utilisateur, cp.date_debut as "mise en service", cp.date_fin as "fin de service"
	FROM clefs c
	LEFT JOIN clefs_personnes cp on c.numero_serie = cp.clefs_numero_serie
       LEFT JOIN personnes p on cp.personnes_uuid = p.uuid
	WHERE 
	to_tsvector('french',coalesce(c.numero_serie, '')) ||
	to_tsvector('french',coalesce(c.status, '')) ||
	to_tsvector('french',coalesce(p.email, ''))
	@@ websearch_to_tsquery('french', 'ACTIVE')
;	

/*
Ajouter une clef :

[annuler] [valider création]

numéro de série: 0000-0000-0000-0009
status: ACTIVE____________________________V
utilisateur: CailletC_____________________V
lieux: CIFOM - Etage 3 - 315______________V [supprimer]
       ___________________________________V
*/

--premièrement, ajout de la clef
INSERT INTO clefs
       (numero_serie,
       status)
VALUES ('0000-0000-0000-0009',
        'ACTIVE')
;


--vérification
SELECT * 
FROM clefs
;

--Attribution de la clef dans le registre :
INSERT INTO clefs_personnes
       (personnes_uuid,
       clefs_numero_serie,
       date_debut)
VALUES ((SELECT uuid FROM personnes
        WHERE nom LIKE 'Caillet' AND 
        email LIKE 'cedric.caillet@rpn.ch' AND
        prenom LIKE 'Cedric'),
        '0000-0000-0000-0009',
        '2022-02-05')
;


--Ajout du groupe au lieu 
INSERT INTO acces
       (groupes_uuid,
        lieux_uuid,
	   date_debut,
	   date_fin)
VALUES((SELECT uuid
		FROM groupes
		WHERE nom LIKE 'cedric.caillet@rpn.ch'), 
	(SELECT uuid 
	FROM lieux
       WHERE nom LIKE '315' 
	AND lieu_parent_uuid = (SELECT uuid
			   	  FROM lieux
				  WHERE nom LIKE 'Etage 3' 
			         AND lieu_parent_uuid = (SELECT uuid
			 				    FROM lieux
							    WHERE nom LIKE 'CIFOM')
                            ) 
	),
	   '2022-01-19',
        '2022-02-23'
      )
;

--vérification
SELECT * 
FROM acces
;


/*
Afficher le détail d'un clef :

[annuler][modifier][supprimer]

numéro de série: 0000-0000-0000-0001
status: ACTIVE
utilisateur: cedric.caillet@rpn.ch
mise en service: 2022-02-05 00:00:00 
lieux d'accès: CIFOM - Etage 3 - 315
*/

SELECT c.numero_serie, c.status, p.email, cp.date_debut AS "mise en service", l.nom
FROM clefs c
JOIN clefs_personnes cp on c.numero_serie = cp.clefs_numero_serie
JOIN personnes p on cp.personnes_uuid = p.uuid
JOIN groupes g on p.groupes_uuid = g.uuid
JOIN acces a on g.uuid = a.groupes_uuid
JOIN lieux l on a.lieux_uuid = l.uuid
WHERE c.numero_serie LIKE '0000-0000-0000-0001'
;

/*
Modifier les infos lié à une clef (ACTIVE --> DISFONCTIONNELLE) :

numéro de série: 0000-0000-0000-0002
status: Active____________V  --> DISFONCTIONNELLE
utilisateur: ToblerC____________________V
lieu: CPLN - Etage 2 - B205_________V [supprimer] 
      ______________________________V

*/

SELECT * 
FROM clefs
;


--UPDATE du status (à retester à cause function)
UPDATE clefs
SET status = 'DISFONCTIONNELLE'
WHERE numero_serie = '0000-0000-0000-0002'
;

--vérification
SELECT * 
FROM clefs
WHERE numero_serie = '0000-0000-0000-0002'
;
/* 
Supprimer une clef :

[annuler] [valider suppression]

numéro de série: 0000-0000-0000-0001
status: ACTIVE
utilisateur: cedric.caillet@rpn.ch
mise en service: 2022-02-05 00:00:00
lieu: CIFOM - Etage 3 - 315
*/

SELECT * 
FROM clefs
;

--suppression
DELETE
FROM clefs
WHERE numero_serie = '0000-0000-0000-0001'
;

--vérification
SELECT * 
FROM clefs
;


/*
Afficher la liste des serrures :

       lieu           | cardinalite |                  numero de serie des clefs                    |           historique d'utilisateur                                |
----------------------+-------------+---------------------------------------------------------------+-------------------------------------------------------------------+
CIFOM - Etage 3 - 315 | NORD        | 0000-0000-0000-0004, 0000-0000-0000-0001                      | dominique.huguenin@rpn.ch, cedric.caillet@rpn.ch                  | (Caillet supprimé plus haut)
CIFOM - Etage 3 - 315 | SUD         | 0000-0000-0000-0004                                           | dominique.huguenin@rpn.ch                                         |
CPLN - Etage 1 - B101 | EST         | 0000-0000-0000-0003, 0000-0000-0000-0002, 0000-0000-0000-0005 | diogo.paivacosta@rpn.ch, cyril.tobler@rpn.ch, darwin.ameli@rpn.ch |
CPLN - Etage 1 - B102 | EST         | 0000-0000-0000-0003, 0000-0000-0000-0005                      | diogo.paivacosta@rpn.ch, darwin.ameli@rpn.ch                      |
CPLN - Etage 2 - B205 | NORD        | 0000-0000-0000-0002                                           | cyril.tobler@rpn.ch                                               |
CPLN - Etage 2 - B206 | SUD         |                                                               |                                                                   |
CPLN - Etage 3 - B302 | EST         |                                                               |                                                                   |
CPLN - Etage 3 - B303 | NORD        |                                                               |                                                                   |
CPLN - Etage 4 - B401 | EST         |                                                               |                                                                   |
*/

--Affichage
SELECT s.uuid, l.nom as lieu, s.cardinalite, h.clefs_numero_serie, p.email
FROM serrures s
LEFT JOIN historique_utilisation h on s.uuid = h.serrures_uuid
JOIN lieux l on s.lieux_uuid = l.uuid
LEFT JOIN clefs c on h.clefs_numero_serie = c.numero_serie
LEFT JOIN clefs_personnes cp on c.numero_serie = cp.clefs_numero_serie
LEFT JOIN personnes p on cp.personnes_uuid = p.uuid
;

/* Rechercher plein texte des champs */
-- Au moins un des champs doit contenir le terme '315'.
SELECT s.uuid, l.nom as lieu, s.cardinalite, h.clefs_numero_serie, p.email
FROM serrures s
LEFT JOIN historique_utilisation h on s.uuid = h.serrures_uuid
JOIN lieux l on s.lieux_uuid = l.uuid
LEFT JOIN clefs c on h.clefs_numero_serie = c.numero_serie
LEFT JOIN clefs_personnes cp on c.numero_serie = cp.clefs_numero_serie
LEFT JOIN personnes p on cp.personnes_uuid = p.uuid
WHERE
to_tsvector('french', coalesce(l.nom, '')) ||
to_tsvector('french', coalesce(s.cardinalite, '')) ||
to_tsvector('french', coalesce(h.clefs_numero_serie,'')) ||
to_tsvector('french', coalesce(p.email,''))
@@ to_tsquery('french', '315')
;


/*
Ajouter une serrure :

uuid: b02813c6-2f5e-4086-9cb3-dc35e16fd504
lieu: CPLN - Etage 1 - B102___V
cardinalité: OUEST____________V
*/

SELECT *
FROM serrures
;
--Ajout de la serrure
INSERT INTO serrures
       (uuid,
       cardinalite,
       lieux_uuid)
VALUES(uuid_generate_v4(),
       'OUEST',
       (SELECT uuid
		FROM lieux
		WHERE nom = 'B102' AND lieu_parent_uuid = (SELECT uuid 
								FROM lieux
								WHERE nom = 'Etage 1' 
								AND lieu_parent_uuid = (SELECT uuid
											  FROM lieux
											  WHERE nom = 'CPLN')
								)
	)
     )
;

--vérification 
SELECT *
FROM serrures
;	
/*
Afficher le détail d'une serrure :

[annuler][modifier][supprimer]

uuid: b02813c6-2f5e-4086-9cb3-dc35e16fd502
lieu: CIFOM - Etage 3 - 315
cardinalité: NORD
clef: 0000-0000-0000-0009
historique d'acces: Dominique Huguenin 2022-01-06 10:16:56
*/

--Afficher les clefs liées à la serrure
SELECT distinct s.uuid, l.nom, s.cardinalite, c.numero_serie
FROM serrures s
JOIN lieux l on s.lieux_uuid = l.uuid
JOIN acces a on l.uuid = a.lieux_uuid
JOIN groupes g on a.groupes_uuid = g.uuid
JOIN personnes p on g.uuid = p.groupes_uuid
JOIN clefs_personnes cp on p.uuid = cp.personnes_uuid
JOIN clefs c on cp.clefs_numero_serie = c.numero_serie
WHERE s.cardinalite = 'NORD' 
AND l.nom = (SELECT nom 
	     FROM lieux
            WHERE nom LIKE '315' 
	     AND lieu_parent_uuid = (SELECT uuid
			   	       FROM lieux
				       WHERE nom LIKE 'Etage 3' 
			              AND lieu_parent_uuid = (SELECT uuid
			 				         FROM lieux
							         WHERE nom LIKE 'CIFOM')
                                   ) 
            )
;
	

--Afficher l'historique des utilisateurs liées à cette serrure
SELECT ph.prenom, ph.nom, h.date_utilisation
FROM serrures s
JOIN lieux l on s.lieux_uuid = l.uuid
JOIN historique_utilisation h on s.uuid = h.serrures_uuid
JOIN clefs ch on h.clefs_numero_serie = ch.numero_serie
JOIN clefs_personnes cp on ch.numero_serie = cp.clefs_numero_serie
JOIN personnes ph on cp.personnes_uuid = ph.uuid
WHERE s.cardinalite = 'NORD' 
AND l.nom = (SELECT nom 
	     FROM lieux
            WHERE nom LIKE '315' 
	     AND lieu_parent_uuid = (SELECT uuid
			   	       FROM lieux
				       WHERE nom LIKE 'Etage 3' 
			              AND lieu_parent_uuid = (SELECT uuid
			 				         FROM lieux
							         WHERE nom LIKE 'CIFOM')
                                   ) 
            )
;

/*
Modification de la cardinalite (en EST) de la serrure NORD de la salle 315:

[annuler] [valider modification]

uuid: b02813c6-2f5e-4086-9cb3-dc35e16fd501
lieu: CIFOM - Etage 3 - 315___V
cardinalité: NORD_____________V

*/

SELECT distinct s.uuid, l.nom, s.cardinalite, s.instant_modification, s.user_modification
FROM serrures s
JOIN lieux l on s.lieux_uuid = l.uuid
WHERE s.cardinalite = 'NORD' 
AND l.nom = (SELECT nom 
	     FROM lieux
            WHERE nom LIKE '315' 
	     AND lieu_parent_uuid = (SELECT uuid
			   	       FROM lieux
				       WHERE nom LIKE 'Etage 3' 
			              AND lieu_parent_uuid = (SELECT uuid
			 				         FROM lieux
							         WHERE nom LIKE 'CIFOM')
                                   ) 
            )
;

--Modification de la cardinalité
UPDATE serrures
SET cardinalite = 'EST'
WHERE cardinalite = 'NORD' and lieux_uuid = (SELECT uuid 
	     FROM lieux
            WHERE nom LIKE '315' 
	     AND lieu_parent_uuid = (SELECT uuid
			   	       FROM lieux
				       WHERE nom LIKE 'Etage 3' 
			              AND lieu_parent_uuid = (SELECT uuid
			 				         FROM lieux
							         WHERE nom LIKE 'CIFOM')
                                   ) 
            )
;

--Vérification
SELECT distinct s.uuid, l.nom, s.cardinalite, s.instant_modification, s.user_modification
FROM serrures s
JOIN lieux l on s.lieux_uuid = l.uuid
WHERE s.cardinalite = 'EST' 
AND l.nom = (SELECT nom 
	     FROM lieux
            WHERE nom LIKE '315' 
	     AND lieu_parent_uuid = (SELECT uuid
			   	       FROM lieux
				       WHERE nom LIKE 'Etage 3' 
			              AND lieu_parent_uuid = (SELECT uuid
			 				         FROM lieux
							         WHERE nom LIKE 'CIFOM')
                                   ) 
            )
;

/*
Suppresion de la serrure NORD de la salle B102 :


[annuler] [valider suppression]

uuid: b02813c6-2f5e-4086-9cb3-dc35e16fd503
lieu: CPLN - Etage 2 - B205
cardinalité: NORD

*/

--Affichage
SELECT distinct s.uuid, l.nom, s.cardinalite, s.instant_modification, s.user_modification
FROM serrures s
JOIN lieux l on s.lieux_uuid = l.uuid
WHERE s.cardinalite = 'NORD' 
AND l.nom = (SELECT nom 
	     FROM lieux
            WHERE nom LIKE 'B205' 
	     AND lieu_parent_uuid = (SELECT uuid
			   	       FROM lieux
				       WHERE nom LIKE 'Etage 2' 
			              AND lieu_parent_uuid = (SELECT uuid
			 				         FROM lieux
							         WHERE nom LIKE 'CPLN')
                                   ) 
            )
;


SELECT * 
FROM serrures
;

--Suppression
DELETE
FROM serrures 
WHERE cardinalite = 'NORD' 
AND lieux_uuid = (SELECT uuid 
	     FROM lieux
          WHERE nom LIKE 'B205' 
	     AND lieu_parent_uuid = (SELECT uuid
			   	       FROM lieux
				       WHERE nom LIKE 'Etage 2' 
			              AND lieu_parent_uuid = (SELECT uuid
			 				         FROM lieux
							         WHERE nom LIKE 'CPLN')
                                   ) 
            )
;

--Vérification
SELECT * 
FROM serrures
;

/*
Afficher la liste des utilisateurs :

[rechercher] : ________________________

[ajouter un utilisateur]


|           action            |     nom     |  prenom   |           groupes             |
+-----------------------------+-------------+-----------+-------------------------------+
| [voir][modifier][supprimer] | Ameli       | Darwin    | Etudiant CIFOM, Etudiant CPLN |
| [voir][modifier][supprimer] | Paiva Costa | Diogo     | Etudiant CIFOM                | 
| [voir][modifier][supprimer] | Tobler      | Cyril     | Etudiant CIFOM                |
| [voir][modifier][supprimer] | Caillet     | Cedric    | Enseignant de prog            |
| [voir][modifier][supprimer] | Huguenin    | Dominique | Enseignant de prog            |
*/

SELECT p.nom, p.prenom, gd.nom as "groupes"
FROM personnes p
JOIN groupes g on p.groupes_uuid = g.uuid
JOIN groupes_groupes gg on g.uuid = gg.enfant_uuid
JOIN groupes gd on gg.parent_uuid = gd.uuid
;

/* Rechercher plein texte des champs */
-- Au moins un des champs doit contenir le terme 'Dominique'.
SELECT p.nom, p.prenom, gd.nom as "groupes"
FROM personnes p
JOIN groupes g on p.groupes_uuid = g.uuid
JOIN groupes_groupes gg on g.uuid = gg.enfant_uuid
JOIN groupes gd on gg.parent_uuid = gd.uuid
WHERE
to_tsvector('french', coalesce(p.nom, '')) ||
to_tsvector('french', coalesce(p.prenom, '')) ||
to_tsvector('french', coalesce(gd.nom,'')) 
@@ to_tsquery('french', 'Dominique')
;


/*
Création d'un utilisateur :

nom: Cover______________________
prenom: Michel__________________
groupe: Enseignants____________V [supprimer]
        _______________________V
clef: 0000-0000-0000-0008______V
e-mail: michel.cover@rpn.ch_____
*/

SELECT *
FROM groupes
;

--Ajout du groupe
INSERT INTO groupes
       (uuid,
       nom,
       description)
VALUES(uuid_generate_v4(),
       'michel.cover@rpn.ch',
       'Groupe personnel')
;


SELECT *
FROM personnes
;

--Ajout de la personne

INSERT INTO personnes
       (uuid,
        nom, 
        prenom,
        email,
        groupes_uuid)
VALUES (uuid_generate_v4(),
        'Cover',
        'Michel',
        'michel.cover@rpn.ch',
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'michel.cover@rpn.ch'))
;
--Vérification
SELECT *
FROM personnes
;

SELECT *
FROM clefs
;

--Activation de la clef
UPDATE clefs
SET status = 'ACTIVE'
WHERE numero_serie = '0000-0000-0000-0004'
;

--Vérification
SELECT *
FROM clefs
;


SELECT *
FROM clefs_personnes
;

--Attribution de la clef
INSERT INTO clefs_personnes
       (personnes_uuid,
       clefs_numero_serie,
       date_debut)
VALUES ((SELECT uuid FROM personnes
        WHERE nom LIKE 'Cover' AND 
        email LIKE 'michel.cover@rpn.ch' AND
        prenom LIKE 'Michel'),
        '0000-0000-0000-0004',
        '2022-02-05')
;

SELECT *
FROM clefs_personnes
;

/*
Afficher le détail de l'utilisateur appelé "Darwin Ameli" :

[annuler][modifier][supprimer]

nom: Ameli
prenom: Darwin
groupe: darwin.ameli@rpn.ch (groupe perso)
clef: 
e-mail: darwin.ameli@rpn.ch
historique d'accès: CPLN - Etage 1 - B101, CPLN - Etage 1 - B102
*/

--Affichage de ces groupes
SELECT p.nom, p.prenom, p.email, g.nom as "groupes", g.description
FROM personnes p
JOIN groupes g on p.groupes_uuid = g.uuid
WHERE p.nom = 'Ameli' and p.prenom = 'Darwin'
;

--Affichage avec historique d'accès
SELECT p.nom, p.prenom, p.email, l.nom as "historique lieux"
FROM personnes p
JOIN clefs_personnes cp on p.uuid = cp.personnes_uuid
JOIN clefs c on cp.clefs_numero_serie = c.numero_serie
JOIN historique_utilisation h on c.numero_serie = h.clefs_numero_serie
JOIN serrures s on h.serrures_uuid = s.uuid
JOIN lieux l on s.lieux_uuid = l.uuid
WHERE p.nom = 'Ameli' and p.prenom = 'Darwin'
;

/*
Modifier l'email de l'utilisateur appelé "Darwin Ameli" 

[annuler] [valider modification]

nom: Ameli_____________________
prenom: Darwin_________________
groupe: darwin.ameli@rpn.ch ___V [supprimer] (groupe perso)
        _______________________V
clef:   _______________________V
e-mail: darwin.ameli@rpn.ch____ --> .com
*/
SELECT *
FROM personnes
;

--Modification de la date de naissance
UPDATE personnes
SET email = 'darwin.ameli@rpn.com'
WHERE email = 'darwin.ameli@rpn.ch'
;

--Vérification
SELECT *
FROM personnes
;
/*
Supprimer l'utilisateur appelé "Darwin Ameli" :

[annuler] [valider suppression]

nom: Ameli
prenom: Darwin
groupe: darwin.ameli@rpn.ch (groupe perso)
clef: 
e-mail: darwin.ameli@rpn.com
*/
SELECT *
FROM personnes
;

--Suppression
DELETE
FROM personnes
WHERE nom = 'Ameli' AND prenom = 'Darwin' AND email = 'darwin.ameli@rpn.com'
;

--Vérification
SELECT *
FROM personnes
;
/*
Afficher la liste des groupes :

| action                      | nom                 |                          description                              | groupe parent     | 
+-----------------------------+---------------------+-------------------------------------------------------------------+-------------------+
| [voir][modifier][supprimer] | Conciergerie        | Groupe contenant tout le personnel de conciergerie des deux sites |                   |
| [voir][modifier][supprimer] | Conciergerie CPLN   | Groupe contenant tout le personnel de conciergerie du CIFOM       | Conciergerie      |
| [voir][modifier][supprimer] | Conciergerie CIFOM  | Groupe contenant tout le personnel de conciergerie du CIFOM       | Conciergerie      |
| [voir][modifier][supprimer] | Enseignants         | Groupe contenant tous les enseignants des deux sites              |                   |
| [voir][modifier][supprimer] | Enseignants de prog | Groupe contenant tous les enseignants de prog des deux sites      | Enseignants       |
| [voir][modifier][supprimer] | Direction           | Groupe contenant les membres de direction des deux sites          |                   |
| [voir][modifier][supprimer] | Direction CPLN      | Groupe contenant les membres de direction du CPLN                 | Direction         |
| [voir][modifier][supprimer] | Direction CIFOM     | Groupe contenant les membres de direction du CIFOM                | Direction         |
| [voir][modifier][supprimer] | Etudiants           | Groupe contenant les etudiants des deux sites                     |                   |
| [voir][modifier][supprimer] | Etudiants CIFOM     | Groupe contenant les etudiants du CIFOM                           | Etudiants         |
| [voir][modifier][supprimer] | Etudiants CPLN      | Groupe contenant les etudiants du CPLN                            | Etudiants         |
*/

SELECT g.nom, g.description, gd.nom as "groupe parent"
FROM groupes g
LEFT JOIN groupes_groupes gg on g.uuid = gg.enfant_uuid
LEFT JOIN groupes gd on gg.parent_uuid = gd.uuid
;

/* Rechercher plein texte des champs */
-- Au moins un des champs doit contenir le terme 'Enseignants'.
SELECT g.nom, g.description, gd.nom as "groupe parent"
FROM groupes g
LEFT JOIN groupes_groupes gg on g.uuid = gg.enfant_uuid
LEFT JOIN groupes gd on gg.parent_uuid = gd.uuid
WHERE
to_tsvector('french', coalesce(g.nom, '')) ||
to_tsvector('french', coalesce(g.description, '')) ||
to_tsvector('french', coalesce(gd.nom,'')) 
@@ to_tsquery('french', 'Enseignants')
;

/*
Créer le groupe "Enseignants d'informatique" :

[annuler] [valider création]

uuid: b02813c6-2f5e-4086-9cb3-dc35e16fd514
nom: Enseignants informatique__________
groupe parent: Enseignants______________V [supprimer]
               _________________________V
description: Groupe contenant tous les enseignants informatique des deux sites__
utilisateurs: cedric.caillet@rpn.ch_____V [supprimer]
              dominique.huguenin@rpn.ch_V [supprimer]
              __________________________V
*/
SELECT *
FROM groupes
;

--Création du groupe
INSERT INTO groupes
       (uuid,
       nom,
       description)
VALUES(uuid_generate_v4(),
       'Enseignants informatique',
       'Groupe contenant tous les enseignants informatique des deux sites')
;

--Vérification
SELECT *
FROM groupes
;

SELECT *
FROM groupes_groupes
;

--Ajout du groupe enseignants informatique dans enseignants

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants informatique'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants'))
;

--Ajout des personnes dans le groupe
INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'cedric.caillet@rpn.ch'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants informatique'))
;

INSERT INTO groupes_groupes
       (enfant_uuid,
        parent_uuid)
VALUES ((SELECT uuid FROM groupes
        WHERE nom LIKE 'dominique.huguenin@rpn.ch'),
        (SELECT uuid FROM groupes
        WHERE nom LIKE 'Enseignants informatique'))
;

--Vérification
SELECT g.nom, g.description, gd.nom as "groupe parent"
FROM groupes g
LEFT JOIN groupes_groupes gg on g.uuid = gg.enfant_uuid
LEFT JOIN groupes gd on gg.parent_uuid = gd.uuid
;

/*
Afficher le détail du groupe "Enseignant de prog" :

[annuler][modifier][supprimer]

uuid: b02813c6-2f5e-4086-9cb3-dc35e16fd511
nom: Enseignants de prog
groupe parent: Enseignants
description: Groupe contenant tous les enseignants de prog
utilisateurs: dominique.huguenin@rpn.ch, cedric.caillet@rpn.ch
*/
--Affichage
SELECT g.uuid, g.nom, g.description, gp.nom as "groupe parent", p.email
FROM groupes g
JOIN groupes_groupes gg on g.uuid = gg.parent_uuid
JOIN groupes gd on gg.enfant_uuid = gd.uuid
JOIN personnes p on gd.uuid = p.groupes_uuid
JOIN groupes_groupes ggp on g.uuid = ggp.enfant_uuid
JOIN groupes gp on ggp.parent_uuid = gp.uuid
where g.nom = 'Enseignants de prog'
;

/*
Modifier la description du groupe appelé "Enseignants de prog" en "Groupe contenant tous les enseignants de programmation des deux sites" :

[annuler] [valider modification]

uuid: b02813c6-2f5e-4086-9cb3-dc35e16fd514
nom: Enseignants de prog
groupe parent: Enseignants______________V
description: Groupe contenant tous les enseignants de prog____
utilisateurs: cedric.caillet@rpn.ch_____V [supprimer]
              dominique.huguenin@rpn.ch_V [supprimer]
              __________________________V
*/

SELECT *
FROM groupes
;

--Modification de la description du groupe
UPDATE groupes
SET description = 'Groupe contenant tous les enseignants de programmation des deux sites'
WHERE nom = 'Enseignants de prog'  
;

--Vérification
SELECT *
FROM groupes
;
/*
Supprimer le groupe appelé "Enseignants de math" :
¨
[annuler] [valider suppression]

uuid: b02813c6-2f5e-4086-9cb3-dc35e16fd511
nom: Enseignants de prog
groupe parent: Enseignants
description: Groupe contenant tous les enseignants de prog
utilisateurs: dominique.huguenin@rpn.ch, cedric.caillet@rpn.ch
*/

SELECT *
FROM groupes
;

--Suppression du groupe
DELETE
FROM groupes
WHERE nom = 'Enseignants de prog'
;

--Vérification
SELECT *
FROM groupes
;

/*
Afficher la liste des lieux :

[ajouter un lieu]


|            action           |           nom           |
+-----------------------------+-------------------------+
| [voir][modifier][supprimer] | CIFOM                   |
| [voir][modifier][supprimer] | CIFOM - Etage 3         |
| [voir][modifier][supprimer] | CIFOM - Etage 3 - 315   |
| [voir][modifier][supprimer] | CPLN                    |
| [voir][modifier][supprimer] | CPLN - Etage 1          |
| [voir][modifier][supprimer] | CPLN - Etage 1 - B101   |
| [voir][modifier][supprimer] | CPLN - Etage 1 - B102   |
| [voir][modifier][supprimer] | CPLN - Etage 2          |
| [voir][modifier][supprimer] | CPLN - Etage 2 - B205   |
| [voir][modifier][supprimer] | CPLN - Etage 2 - B206   |
| [voir][modifier][supprimer] | CPLN - Etage 3          |
| [voir][modifier][supprimer] | CPLN - Etage 3 - B302   |
| [voir][modifier][supprimer] | CPLN - Etage 3 - B303   |
| [voir][modifier][supprimer] | CPLN - Etage 4          |
| [voir][modifier][supprimer] | CPLN - Etage 4 - B401   |
*/

SELECT nom 
FROM lieux
;

/* TODO: Ajouter les lieux recursives à la recherche plein texte */

/* Rechercher plein texte des champs */
-- Au moins un des champs doit contenir le terme 'CPLN'.
SELECT l.nom 
FROM lieux l
WHERE
to_tsvector('french', coalesce(l.nom, ''))
@@ to_tsquery('french', 'CPLN')
OR lieu_parent_uuid = 
 	(SELECT uuid
	FROM lieux
	WHERE nom = 'CPLN')
;


/*
Créer le lieu "301" avec comme parent "CIFOM - Etage 3" :

[annuler] [valider création]

nom: 301_____________________
lieu parent: CIFOM - Etage 3__V
*/

SELECT *
FROM lieux
;

--Ajout du lieu
INSERT INTO lieux
       (uuid,
        nom,
        lieu_parent_uuid)
VALUES(uuid_generate_v4(), 
       '301',
       (SELECT uuid
	FROM lieux 
	WHERE nom = 'Etage 3' 
	AND lieu_parent_uuid = (SELECT uuid
				  FROM lieux
				  WHERE nom = 'CIFOM')))
;

--Vérification
SELECT *
FROM lieux
;

/*
Afficher le détail du lieu  "CPLN" :

[annuler][modifier][supprimer]

nom: CPLN
lieu parent:
*/

SELECT l.nom, lieu_parent_uuid
FROM lieux l
WHERE l.nom = 'CPLN'
;

/*
Modifier le nom du lieu "B101" en "B103" :

[annuler] [valider modification]

nom: B101___________________ --> B103
lieu parent: CPLN - Etage 1__V
*/
SELECT *
FROM lieux
;

--Modification du nom du lieu
UPDATE lieux
SET nom = 'B103'
WHERE nom = 'B101' and lieu_parent_uuid = (SELECT uuid
			   	              FROM lieux
				              WHERE nom LIKE 'Etage 1' 
			                     AND lieu_parent_uuid = (SELECT uuid
			 			       	         FROM lieux
							                WHERE nom LIKE 'CPLN')
                                   )
;
--Vérification
SELECT *
FROM lieux
;

/*
Supprimer le lieu "Etage 1" :

[annuler] [valider suppression]

nom: CPLN - Etage 1
lieu parent: CPLN
*/

SELECT *
FROM lieux
;

--Suppression
DELETE 
FROM lieux
WHERE nom = 'Etage 1' and lieu_parent_uuid = (SELECT uuid
						   FROM lieux
			  			   WHERE nom = 'CPLN')
;

--Vérification
SELECT *
FROM lieux
;

/*Afficher l'historique des utilisations :*/

/*
[rechercher] : ________________________

| acces  |           lieu         | cardinalite |    numero_serie     |    utilisateur      |
+--------+------------------------+-------------+---------------------+---------------------+
| Refusé | CIFOM - Etage 3 - 315  | SUD         | 0000-0000-0000-0004 | Huguenin Dominique  |
| Refusé | CIFOM - Etage 3 - 315  | EST         | 0000-0000-0000-0004 | Huguenin Dominique  |
| Refusé | CIFOM - Etage 3 - 315  | SUD         | 0000-0000-0000-0004 | Cover Michel        |
| Refusé | CIFOM - Etage 3 - 315  | EST         | 0000-0000-0000-0004 | Cover Michel        |
*/

SELECT (CASE WHEN h.status=true THEN 'Réussi' ELSE 'Refusé'  END) as "accès", l.nom as "lieu",s.cardinalite, c.numero_serie, CONCAT(p.nom, ' ', p.prenom) as utilisateur 
FROM personnes p
JOIN clefs_personnes cp on p.uuid = cp.personnes_uuid
JOIN clefs c on cp.clefs_numero_serie = c.numero_serie
JOIN historique_utilisation h on c.numero_serie = h.clefs_numero_serie
JOIN serrures s on h.serrures_uuid = s.uuid
JOIN lieux l on s.lieux_uuid = l.uuid
;
