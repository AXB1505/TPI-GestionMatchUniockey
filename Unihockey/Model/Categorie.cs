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
        private string strNom;
        
        private PostgresBdService _db = new PostgresBdService();

        public string getNom()
        {
            return strNom;
        }

        public Categorie()
        {
        }

        public Categorie(string nom)
        {
            strNom = nom;
        }

        public List<Categorie> GetList()
        {
            List<Categorie> categories = new List<Categorie>();
            _db.OpenConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM categorie", _db.GetConnection());

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Categorie c = new Categorie(reader["nom"].ToString());
                categories.Add(c);
            }

            _db.CloseConnection();

            return categories;
        }

        public int getId()
        {
            int id = 0;

            _db.OpenConnection();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT num FROM categorie WHERE nom='" + strNom + "';", _db.GetConnection());

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }

            _db.CloseConnection();

            return id;
        }

        public override string ToString()
        {
            return strNom;
        }
    }
}
