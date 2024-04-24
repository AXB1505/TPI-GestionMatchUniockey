using Npgsql;
using System.Diagnostics;
using Unihockey.Model;
using Npgsql;
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

            const string DB_CONNECTION_STRING = "Server=localhost;Port=5432;Database=unihockey;User Id=postgres;Password=TPI;";

            NpgsqlConnection conn = new NpgsqlConnection(DB_CONNECTION_STRING);

            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT nom FROM categorie", conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            string nom = "";

            while (dr.Read())
            {
                nom = dr.GetString(0);
                Debug.WriteLine(nom);
            }
            dr.Close();

        }
    }

}
