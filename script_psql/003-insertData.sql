/*
    @Author : Alessandro Borrani
    Code sql pour l'insertion de données de test dans la BD
*/
-- Insertion de données
-- Lieu
-- Categorie
INSERT INTO Categorie(num, nom, dureeMatch, nombrePeriode) VALUES
    (1, 'Juniors E', '24000', 1),
    (2, 'Juniors D', '20000', 2),
    (3, 'Juniors C', '24000', 2),
    (4, 'Juniors B', '20000', 2),
    (5, 'Juniors A', '20000', 2),
    (6, 'Femme', '20000', 2),
    (7, 'Homme', '20000', 2)
;
-- Equipe
INSERT INTO Equipe(num, nom, Cat_num) VALUES
    (1, 'Fleurier Unihockey Club', 1),
    (2, 'Klub FSC Corcelles-Cormondrèche', 1),
    (3, 'UHC Le Rouge et Or', 1),
    (4, 'UHC Bevaix', 1),
    (5, 'UHC Cornaux', 1),
    (6, 'UHC La Brévine', 1),
    (7, 'UHC La Chaux-de-Fonds', 1)
;
-- Resultat
INSERT INTO Match(num, Equ_num1, Equ_num2, debutMatch, scoreEquipe1, scoreEquipe2) VALUES
    (1, 1, 2, '2024-01-01 14:00:00', 2, 1),
    (2, 3, 4, '2024-01-02 14:00:00', 3, 0),
    (3, 5, 6, '2024-01-03 14:00:00', 1, 1),
    (4, 7, 3, '2024-01-04 14:00:00', 0, 2),
    (5, 2, 7, '2024-01-05 14:00:00', 1, 1)
;