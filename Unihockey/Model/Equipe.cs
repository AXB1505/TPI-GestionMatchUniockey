using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unihockey.Model
{
    internal class Equipe
    {
        // Instanciation des variables en fonction des colonnes de la table Equipe de la base de données
        private string strNom = "";
        private Categorie _categorie = new Categorie();

        // Instanciation de la connexion à la base de données
        private PostgresBdService _db = new PostgresBdService();

        // Constructeur vide
        public Equipe()
        {
        }

        // Constructeur avec paramètres
        public Equipe(string nom, Categorie categorie)
        {
            strNom = nom;
            _categorie = categorie;
        }

        // Méthode pour obtenir le nom de l'équipe
        public string getNom()
        {
            return strNom;
        }

        // Méthode pour obtenir la catégorie de l'équipe
        public Categorie getCategorie()
        {
            return _categorie;
        }

        // Méthode pour obtenir la liste des équipes
        public List<Equipe> GetList()
        {
            List<Equipe> equipes = new List<Equipe>();

            // Instanciation de la requête pour obtenir toutes les équipes de la base de données
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Equipe", _db.GetConnection());

            _db.OpenConnection();

            // Exécution de la requête
            NpgsqlDataReader reader = cmd.ExecuteReader();

            // Boucle pour ajouter chaque équipe à la liste avec le retour de la requête
            while (reader.Read())
            {
                equipes.Add(new Equipe(reader["nom"].ToString(), new Categorie(_categorie.GetList()[(int)reader["cat_num"] - 1].getNom())));
            }

            _db.CloseConnection();

            return equipes;
        }

        // Méthode pour créer une équipe
        public void Create()
        {
            // Instanciation de la requête pour ajouter une équipe à la base de données
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO Equipe(nom, cat_num) VALUES('" + this.getNom() +"', '" + this.getCategorie().getId() + "');", _db.GetConnection());

            _db.OpenConnection();

            // Exécution de la requête
            cmd.ExecuteNonQuery();

            _db.CloseConnection();
        }

        // Méthode pour obtenir l'ID de l'équipe
        public int getId()
        {
            int id = 0;

            // Instanciation de la requête pour obtenir l'ID de l'équipe en fonction de son nom
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT num FROM Equipe WHERE nom='" + strNom + "';", _db.GetConnection());

            _db.OpenConnection();

            // Exécution de la requête
            NpgsqlDataReader reader = cmd.ExecuteReader();

            // Lecture du retour de la requête et assignation de l'ID
            reader.Read();
            id = reader.GetInt32(0);

            _db.CloseConnection();

            return id;
        }

        // Méthode pour modifier une équipe
        public void Edit(Equipe equipe)
        {
            // Instanciation de la requête pour modifier une équipe de la base de données en fonction de son ID
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE Equipe SET nom='" + equipe.getNom() + "', cat_num='" + equipe.getCategorie().getId() + "' WHERE num=" + this.getId() + ";", _db.GetConnection());

            _db.OpenConnection();

            // Exécution de la requête
            cmd.ExecuteNonQuery();

            _db.CloseConnection();
        }

        // Méthode pour supprimer une équipe
        public void Delete()
        {
            // Instanciation de la requête pour supprimer une équipe de la base de données en fonction de son ID
            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM Equipe WHERE num=" + this.getId() + ";", _db.GetConnection());

            _db.OpenConnection();
            
            cmd.ExecuteNonQuery();

            _db.CloseConnection();
        }

        // Méthode pour obtenir l'équipe en format string
        public override string ToString()
        {
            return $"{strNom} ({_categorie.getNom()})";
        }
    }
}
