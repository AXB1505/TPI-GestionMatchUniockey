using Unihockey.Model;

namespace Unihockey.Pages;

public partial class Accueil : ContentPage
{
	public Accueil()
	{
		InitializeComponent();
	}

    // M�thode pour cr�er un match de 1 p�riode de 24 minutes
	private void OnbtnCreerMatch1x24Clicked(object sender, EventArgs e)
	{
        // D�finition des param�tres du match
		int iPeriode = 1;
		int iDureePeriode = 24;

        // Ouverture de la page de gestion de match avec les bon param�tres
		Navigation.PushAsync(new GestionMatch(iPeriode, iDureePeriode, new Categorie("Juniors E")));
    }

    // M�thode pour cr�er un match de 2 p�riodes de 20 minutes
    private async void OnbtnCreerMatch2x20Clicked(object sender, EventArgs e)
    {
        // D�finition des param�tres du match
        int iPeriode = 2;
        int iDureePeriode = 20;

        // Demande de la cat�gorie du match � l'utilisateur
        string strRep = await DisplayActionSheet("Choisir la cat�gorie", "Annuler", null, "Juniors D", "Juniors C", "Juniors B", "Juniors A", "Homme", "Femme");
        // Si l'utilisateur annule ou ferme la fen�tre, l'application ne fait rien
        if (strRep == "Annuler" || strRep == null)
        {
            return;
        }

        // Ouverture de la page de gestion de match avec les bon param�tres
        Navigation.PushAsync(new GestionMatch(iPeriode, iDureePeriode, new Categorie(strRep)));
    }
}