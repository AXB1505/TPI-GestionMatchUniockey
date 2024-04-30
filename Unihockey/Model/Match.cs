using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Npgsql;

namespace Unihockey.Model
{
    internal class Match
    {
        private Equipe equEquipe1;
        private Equipe equEquipe2;
        private DateTime dtDebutMatch;
        private int iScoreEquipe1;
        private int iScoreEquipe2;

        public string Equipe1 { get => equEquipe1.getNom(); }
        public string Equipe2 { get => equEquipe2.getNom(); }
        public string DebutMatch { get => dtDebutMatch.ToString(); }
        public string ScoreEquipe1 { get => iScoreEquipe1.ToString(); }
        public string ScoreEquipe2 { get => iScoreEquipe2.ToString(); }

    PostgresBdService _db = new PostgresBdService();

        public Match() { }

        public Match(Equipe equipe1, Equipe equipe2, int scoreEquipe1, int scoreEquipe2)
        {
            equEquipe1 = equipe1;
            equEquipe2 = equipe2;
            iScoreEquipe1 = scoreEquipe1;
            iScoreEquipe2 = scoreEquipe2;
            dtDebutMatch = DateTime.Now;
        }

        public Match(Equipe equipe1, Equipe equipe2, int scoreEquipe1, int scoreEquipe2, DateTime debutMatch)
        {
            equEquipe1 = equipe1;
            equEquipe2 = equipe2;
            iScoreEquipe1 = scoreEquipe1;
            iScoreEquipe2 = scoreEquipe2;
            dtDebutMatch = debutMatch;
        }

        public string getEquipe1()
        {
            return equEquipe1.getNom();
        }
        public Equipe getEquipe2()
        {
               return equEquipe2;
        }

        public DateTime getDate()
        {
            return dtDebutMatch;
        }
        public int getScoreEquipe1()
        {
               return iScoreEquipe1;
        }
        public int getScoreEquipe2()
        {
               return iScoreEquipe2;
        }

        public List<Match> getList()
        {
            List<Match> matches = new List<Match>();
            // Instanciation de la requête pour obtenir tous les matchs de la base de données
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT Equipe1.nom as equipe1, Equipe2.nom as equipe2, " +
                "Match.scoreEquipe1 as scoreEquipe1, Match.scoreEquipe2 as scoreEquipe2, Match.debutMatch as " +
                "debutMatch FROM Match, Equipe Equipe1, Equipe Equipe2 Where Match.Equ_num1 = Equipe1.num AND " +
                "Equ_num2 = Equipe2.num;", _db.GetConnection());

            _db.OpenConnection();

            // Exécution de la requête
            NpgsqlDataReader reader = cmd.ExecuteReader();

            // Boucle pour ajouter chaque match à la liste avec le retour de la requête
            while (reader.Read())

            {
                matches.Add(new Match(new Equipe().GetList().Find(x => x.getNom() == reader["equipe1"].ToString()), 
                    new Equipe().GetList().Find(x => x.getNom() == reader["equipe2"].ToString()), 
                    (int)reader["scoreEquipe1"], (int)reader["scoreEquipe2"], (DateTime)reader["debutMatch"]));
            }

            return matches;
        }

        public override string ToString()
        {
            return $"{equEquipe1.getNom()} vs {equEquipe2.getNom()}, le {dtDebutMatch.ToString()}, score: {iScoreEquipe1}:{iScoreEquipe2}";
        }
    }
}
