using Unihockey.Model;

namespace Unihockey.Pages;

public partial class Accueil : ContentPage
{
	public Accueil()
	{
		InitializeComponent();
	}

    // Méthode pour créer un match de 1 période de 24 minutes
	private void OnbtnCreerMatch1x24Clicked(object sender, EventArgs e)
	{
        // Définition des paramètres du match
		int iPeriode = 1;
		int iDureePeriode = 24;

        // Ouverture de la page de gestion de match avec les bon paramètres
		Navigation.PushAsync(new GestionMatch(iPeriode, iDureePeriode, new Categorie("Juniors E")));
    }

    // Méthode pour créer un match de 2 périodes de 20 minutes
    private async void OnbtnCreerMatch2x20Clicked(object sender, EventArgs e)
    {
        // Définition des paramètres du match
        int iPeriode = 2;
        int iDureePeriode = 20;

        // Demande de la catégorie du match à l'utilisateur
        string strRep = await DisplayActionSheet("Choisir la catégorie", "Annuler", null, "Juniors D", "Juniors C", "Juniors B", "Juniors A", "Homme", "Femme");
        // Si l'utilisateur annule ou ferme la fenêtre, l'application ne fait rien
        if (strRep == "Annuler" || strRep == null)
        {
            return;
        }

        // Ouverture de la page de gestion de match avec les bon paramètres
        Navigation.PushAsync(new GestionMatch(iPeriode, iDureePeriode, new Categorie(strRep)));
    }
}