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
        private string strNom = "";
        private Categorie _categorie = new Categorie();

        private PostgresBdService _db = new PostgresBdService();

        public Equipe()
        {
        }

        public Equipe(string nom, Categorie categorie)
        {
            strNom = nom;
            _categorie = categorie;
        }

        public string getNom()
        {
            return strNom;
        }

        public Categorie getCategorie()
        {
            return _categorie;
        }

        public List<Equipe> GetList()
        {
            List<Equipe> equipes = new List<Equipe>();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Equipe", _db.GetConnection());

            _db.OpenConnection();

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Equipe e = new Equipe(reader["nom"].ToString(), new Categorie(_categorie.GetList()[(int)reader["cat_num"]-1].getNom()));
                equipes.Add(e);
            }

            _db.CloseConnection();

            return equipes;
        }

        public void Create()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO Equipe(nom, cat_num) VALUES('" + this.getNom() +"', '" + this.getCategorie().getId() + "');", _db.GetConnection());

            _db.OpenConnection();

            cmd.ExecuteNonQuery();

            _db.CloseConnection();
        }

        public void Edit(Equipe equipe)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE Equipe SET nom='" + equipe.getNom() + "', cat_num='" + equipe.getCategorie().getId() + "' WHERE num=" + this.getId() + ";", _db.GetConnection());

            _db.OpenConnection();

            cmd.ExecuteNonQuery();

            _db.CloseConnection();
        }

        public void Delete()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM Equipe WHERE num=" + this.getId() + ";", _db.GetConnection());

            _db.OpenConnection();
            
            cmd.ExecuteNonQuery();

            _db.CloseConnection();
        }

        public int getId()
        {
            int id = 0;

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT num FROM Equipe WHERE nom='" + strNom + "';", _db.GetConnection());

            _db.OpenConnection();

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
            return $"{strNom} ({_categorie.getNom()})";
        }
    }
}
