using Unihockey.Model;

namespace Unihockey.Pages;

public partial class ModifierEquipe : ContentPage
{
	Equipe equEquipe = new Equipe();

    ListView lvListView = new ListView();

	public ModifierEquipe(object equipe, ListView listView)
	{
        InitializeComponent();
        equEquipe = (Equipe)equipe;
        lvListView = listView;
        pickCategorie.ItemsSource = new Categorie().GetList();
        pickCategorie.SelectedIndex = equEquipe.getCategorie().getId()-1;
        lblEquipeAModifier.Text = equEquipe.getNom();
        tbxNom.Text = equEquipe.getNom();
    }

    private void OnbtnConfirmerClicked(object sender, EventArgs e)
    {
        if(tbxNom.Text == null || pickCategorie.SelectedItem == null)
        {
            DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            return;
        }
        Equipe equNewEquipe = new Equipe(tbxNom.Text, (Categorie)pickCategorie.SelectedItem);

        try
        {
            equEquipe.Edit(equNewEquipe);
        }
        catch (Exception ex)
        {
            DisplayAlert("Erreur", ex.Message, "OK");
            return;
        }

        lvListView.ItemsSource = new Equipe().GetList();
        Navigation.PopAsync();
    }
}