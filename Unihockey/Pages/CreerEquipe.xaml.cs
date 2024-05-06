using Unihockey.Model;

namespace Unihockey.Pages;

public partial class CreerEquipe : ContentPage
{
	ListView lvListView = new ListView();

	public CreerEquipe(ListView listView)
	{
		InitializeComponent();
        lvListView = listView;

        pickCategorie.ItemsSource = new Categorie().GetList();
		pickCategorie.SelectedIndex = 0;

    }

	private void OnbtnConfirmerClicked(object sender, EventArgs e)
	{
        Equipe equEquipe = new Equipe(tbxNom.Text, (Categorie)pickCategorie.SelectedItem);
        try
        {
            equEquipe.Create();
        }
        catch
        {
            DisplayAlert("Erreur de saisie", $"Il existe déjà une entrée {equEquipe}", "OK");
            return;
        }
        lvListView.ItemsSource = new Equipe().GetList();
        Navigation.PopAsync();
    }
}