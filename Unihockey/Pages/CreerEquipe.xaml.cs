using Unihockey.Model;

namespace Unihockey.Pages;

public partial class CreerEquipe : ContentPage
{
    // ListView pour rafraichir la liste des équipes dans la page précédente
	ListView lvListView = new ListView();

	public CreerEquipe(ListView listView)
	{
		InitializeComponent();
        // Insértion des données reçues en paramètre dans la listeView
        lvListView = listView;

        // Remplissage de la liste des catégories
        pickCategorie.ItemsSource = new Categorie().GetList();
		pickCategorie.SelectedIndex = 0;

    }

    // Méthode exécuté par le bouton de confirmation pour créer l'équipe
	private void OnbtnConfirmerClicked(object sender, EventArgs e)
	{
        // Création de l'objet Equipe avec les données saisies par l'utilisateur
        Equipe equEquipe = new Equipe(tbxNom.Text, (Categorie)pickCategorie.SelectedItem);

        // Tentative d'ajout de l'équipe dans la base de données
        try
        {
            equEquipe.Create();
        }
        // Envoie d'un message d'erreur si l'équipe existe déjà
        catch
        {
            DisplayAlert("Erreur de saisie", $"Il existe déjà une entrée {equEquipe}", "OK");
            return;
        }

        // Rafraichissement de la liste des équipes dans la page précédente et retour à celle-ci
        lvListView.ItemsSource = new Equipe().GetList();
        Navigation.PopAsync();
    }
}