/*
    @Author : Alessandro Borrani
    Requête pour afficher les résultat d'un match entre 2 équipe en affichant également la catégorie et le lieu de l'équipe, en ayant des alias pertinents
*/

SELECT 
    R.num AS "Numéro du match",
    E1.nom AS "Nom de l'équipe 1",
    E2.nom AS "Nom de l'équipe 2",
    R.debutMatch AS "Date et heure du match",
    R.scoreEquipe1 AS "Score de l'équipe 1",
    R.scoreEquipe2 AS "Score de l'équipe 2",
    C.nom AS "Catégorie de l'équipe 1"
FROM Match R
JOIN Equipe E1 ON R.Equ_num1 = E1.num
JOIN Equipe E2 ON R.Equ_num2 = E2.num
JOIN Categorie C ON E1.Cat_num = C.num
WHERE R.num = 3
;
