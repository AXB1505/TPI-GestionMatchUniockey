using Npgsql;

namespace Unihockey.Model
{
    internal class Match
    {
        // Instanciation des attributs en fonction des colonnes de la table Catégorie de la base de données
        private Equipe equEquipe1;
        private Equipe equEquipe2;
        private DateTime dtDebutMatch;
        private int iScoreEquipe1;
        private int iScoreEquipe2;

        // Paramètres pour le Binding (liaision) de données dans l'affichage des résultats des matchs (ResultatsMatchs)
        public string Equipe1 { get => getEquipe1().getNom(); }
        public string Equipe2 { get => getEquipe2().getNom(); }
        public string DebutMatch { get => getDateDebut().ToString("dd'.'MM'.'yyyy' 'HH':'mm':'ss"); }
        public string ScoreEquipe1 { get => getScoreEquipe1().ToString(); }
        public string ScoreEquipe2 { get => getScoreEquipe2().ToString(); }

        private PostgresBdService _db = new PostgresBdService();

        // Constructeur vide
        public Match() { }

        // Constructeur avec paramètres
        public Match(Equipe equipe1, Equipe equipe2, int scoreEquipe1, int scoreEquipe2, DateTime debutMatch)
        {
            equEquipe1 = equipe1;
            equEquipe2 = equipe2;
            iScoreEquipe1 = scoreEquipe1;
            iScoreEquipe2 = scoreEquipe2;
            dtDebutMatch = debutMatch;
        }

        // Méthodes pour obtenir l'equipe1, l'equipe2, la date de début, le score de l'équipe 1 et le score de l'équipe 2
        public Equipe getEquipe1()
        {
            return equEquipe1;
        }
        public Equipe getEquipe2()
        {
               return equEquipe2;
        }

        public DateTime getDateDebut()
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
                "Equ_num2 = Equipe2.num ORDER BY debutMatch DESC;", _db.GetConnection());

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

        public void Create()
        {
            // Instanciation de la requête pour ajouter un match à la base de données
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO Match (Equ_num1, Equ_num2, scoreEquipe1, scoreEquipe2, debutMatch) " +
                               "VALUES ("+ this.getEquipe1().getId() +"," + this.getEquipe2().getId() + "," + this.getScoreEquipe1() +
                               "," + this.getScoreEquipe2() + "," + "'" + this.getDateDebut().ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss") + "'" + ");", _db.GetConnection());

            _db.OpenConnection();

            // Exécution de la requête
            cmd.ExecuteNonQuery();

            _db.CloseConnection();
        }


        public override string ToString()
        {
            return $"{equEquipe1.getNom()} vs {equEquipe2.getNom()}, le {dtDebutMatch.ToString()}, score: {iScoreEquipe1}:{iScoreEquipe2}";
        }
    }
}
