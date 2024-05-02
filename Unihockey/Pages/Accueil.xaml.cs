using Unihockey.Model;

namespace Unihockey.Pages;

public partial class Accueil : ContentPage
{
	public Accueil()
	{
		InitializeComponent();
	}


	private void OnbtnCreerMatch1x24Clicked(object sender, EventArgs e)
	{
		int iPeriode = 1;
		int iDureePeriode = 24;
		Navigation.PushAsync(new GestionMatch(iPeriode, iDureePeriode, new Categorie("Juniors E")));
    }

    private async void OnbtnCreerMatch2x20Clicked(object sender, EventArgs e)
    {
        int iPeriode = 2;
        int iDureePeriode = 20;

        string strRep = await DisplayActionSheet("Choisir la catégorie", "Annuler", null, "Juniors D", "Juniors C", "Juniors B", "Juniors A", "Homme", "Femme");

        if (strRep == "Annuler" || strRep == null)
        {
            return;
        }

        Categorie catCategorie = new Categorie(strRep);

        Navigation.PushAsync(new GestionMatch(iPeriode, iDureePeriode, catCategorie));
    }
}