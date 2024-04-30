using Npgsql;
using System.Diagnostics;
using Unihockey.Model;
namespace Unihockey.Pages
{
    public partial class MainPage : ContentPage
    {
        private PostgresBdService _db = new PostgresBdService();

        int count = 0;


        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

            //Fonctionnel
            /*
            const string DB_CONNECTION_STRING = "Server=localhost;Port=5432;Database=unihockey;User Id=postgres;Password=TPI;";

            NpgsqlConnection conn = new NpgsqlConnection(DB_CONNECTION_STRING);

            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM categorie", conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            string nom = "";

            while (dr.Read())
            {
                nom = dr["nom"].ToString();
                Debug.WriteLine(nom);
            }
            dr.Close();
            */

            /*                                                  EXEMPLE D'UPDATE D'EQUIPE
            Equipe equ = new Equipe();
            List<Equipe> cs = equ.GetList();

            foreach (Equipe eq in cs)
            {
                Debug.WriteLine("");
            }

            Equipe nEquipe = new Equipe("UHC Les Brenets", new Categorie("Juniors A"));

            nEquipe.Edit(new Equipe("ITALIE", new Categorie("Juniors B")));
            */

            Match match = new Match();
            List<Match> matchs = match.getList();

            foreach (Match m in matchs)
            {
                Debug.WriteLine(m.ToString());
            }
        }
    }

}
