using Unihockey.Model;

namespace Unihockey.Pages;

public partial class CreerEquipe : ContentPage
{
    // ListView pour rafraichir la liste des �quipes dans la page pr�c�dente
	ListView lvListView = new ListView();

	public CreerEquipe(ListView listView)
	{
		InitializeComponent();
        // Ins�rtion des donn�es re�ues en param�tre dans la listeView
        lvListView = listView;

        // Remplissage de la liste des cat�gories
        pickCategorie.ItemsSource = new Categorie().GetList();
		pickCategorie.SelectedIndex = 0;

    }

    // M�thode ex�cut� par le bouton de confirmation pour cr�er l'�quipe
	private void OnbtnConfirmerClicked(object sender, EventArgs e)
	{
        // Cr�ation de l'objet Equipe avec les donn�es saisies par l'utilisateur
        Equipe equEquipe = new Equipe(tbxNom.Text, (Categorie)pickCategorie.SelectedItem);

        // Tentative d'ajout de l'�quipe dans la base de donn�es
        try
        {
            equEquipe.Create();
        }
        // Envoie d'un message d'erreur si l'�quipe existe d�j�
        catch
        {
            DisplayAlert("Erreur de saisie", $"Il existe d�j� une entr�e {equEquipe}", "OK");
            return;
        }

        // Rafraichissement de la liste des �quipes dans la page pr�c�dente et retour � celle-ci
        lvListView.ItemsSource = new Equipe().GetList();
        Navigation.PopAsync();
    }
}