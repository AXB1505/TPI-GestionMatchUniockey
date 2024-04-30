/*
    @Author : Alessandro Borrani
    Code sql pour l'insertion de données de test dans la BD
*/

-- Insertion de données
-- Lieu
-- Categorie

INSERT INTO Categorie(nom) VALUES
    ('Juniors E' ),
    ('Juniors D'),
    ('Juniors C'),
    ('Juniors B'),
    ('Juniors A'),
    ('Femme'),
    ('Homme')
;

-- Equipe
INSERT INTO Equipe(nom, Cat_num) VALUES
    ('Fleurier Unihockey Club', 1),
    ('Klub FSC Corcelles-Cormondrèche', 1),
    ('UHC Le Rouge et Or', 1),
    ('UHC Bevaix', 1),
    ('UHC Cornaux', 1),
    ('UHC La Brévine', 1),
    ('UHC La Chaux-de-Fonds', 1)
;

-- Resultat
INSERT INTO Match(Equ_num1, Equ_num2, debutMatch, scoreEquipe1, scoreEquipe2) VALUES
    (1, 2, '2024-01-01 14:00:00', 2, 1),
    (3, 4, '2024-01-02 14:00:00', 3, 0),
    (5, 6, '2024-01-03 14:00:00', 1, 1),
    (7, 3, '2024-01-04 14:00:00', 0, 2),
    (2, 7, '2024-01-05 14:00:00', 1, 1)
;