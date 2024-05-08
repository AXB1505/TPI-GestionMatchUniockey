using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Unihockey.Model
{
    internal class PostgresBdService
    {
        // Chaîbe de cgarectère pour la connexion à la base de données
        private const string DB_CONNECTION_STRING = "Server=localhost;Port=5432;Database=unihockey;User Id=postgres;Password=TPI;";

        // Instanciation de la connexion à la base de données
        private NpgsqlConnection conn = new NpgsqlConnection(DB_CONNECTION_STRING);

        // Constructeur vide
        public PostgresBdService() { }

        // Méthode pour obtenir l'instance de connexion à la base de données
        public NpgsqlConnection GetConnection()
        {
            return conn;
        }

        // Méthode pour ouvrir la connexion à la base de données
        public void OpenConnection()
        {
            conn.Open();
        }

        // Méthode pour fermer la connexion à la base de données
        public void CloseConnection()
        {
            conn.Close();
        }
    }
}
