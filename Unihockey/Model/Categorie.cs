using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unihockey.Model
{
    internal class Categorie
    {
        // Instanciation des variables en fonction des colonnes de la table Catégorie de la base de données
        private string strNom;

        // Instanciation de la connexion à la base de données
        private PostgresBdService _db = new PostgresBdService();

        // Méthode pour obtenir le nom de la catégorie
        public string getNom()
        {
            return strNom;
        }

        // Constructeur vide
        public Categorie()
        {
        }

        // Constructeur avec paramètre
        public Categorie(string nom)
        {
            strNom = nom;
        }

        // Méthode pour obtenir la liste des catégories
        public List<Categorie> GetList()
        {
            List<Categorie> categories = new List<Categorie>();
            _db.OpenConnection();

            // Instanciation de la requête pour obtenir toutes les catégories de la base de données
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM categorie", _db.GetConnection());

            // Exécution de la requête
            NpgsqlDataReader reader = cmd.ExecuteReader();

            // Boucle pour ajouter chaque catégorie à la liste avec le retour de la requête
            while (reader.Read())
            {
                Categorie c = new Categorie(reader["nom"].ToString());
                categories.Add(c);
            }

            _db.CloseConnection();

            return categories;
        }

        // Méthode pour obtenir l'ID de la catégorie
        public int getId()
        {
            int id = 0;

            _db.OpenConnection();

            // Instanciation de la requête pour obtenir l'ID de la catégorie en fonction du nom
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT num FROM categorie WHERE nom='" + strNom + "';", _db.GetConnection());

            // Exécution de la requête
            NpgsqlDataReader reader = cmd.ExecuteReader();

            // Lecture du retour de la requête et assignation de l'ID
            reader.Read();
            id = reader.GetInt32(0);

            _db.CloseConnection();

            return id;
        }

        public void getCategorieByID(int id)
        {
            Categorie c = new Categorie();
            _db.OpenConnection();

            // Instanciation de la requête pour obtenir la catégorie en fonction de l'ID
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM categorie WHERE num=" + id + ";", _db.GetConnection());

            // Exécution de la requête
            NpgsqlDataReader reader = cmd.ExecuteReader();

            // Lecture du retour de la requête et assignation de la catégorie
            reader.Read();
            c.strNom = reader["nom"].ToString();

            _db.CloseConnection();
        }


        // Méthode pour obtenir la catégorie en format string
        public override string ToString()
        {
            return strNom;
        }
    }
}
