using Unihockey.Model;

namespace Unihockey.Pages;

public partial class GestionEquipes : ContentPage
{
	public GestionEquipes()
	{
		InitializeComponent();
		lvEquipes.ItemsSource = new Equipe().GetList();
    }

    private void OnbtnCreerClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreerEquipe(lvEquipes));
    }

    private void OnbtnRechargerClicked(object sender, EventArgs e)
    {
        lvEquipes.ItemsSource = new Equipe().GetList();
    }

    private async void OnlvEquipesItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
        if (e.SelectedItem != null)
		{
            Equipe equipe = (Equipe)e.SelectedItem;
			string rep = await DisplayActionSheet("Actions :", "Cancel", null, "Modifier", "Supprimer");
            switch (rep)
            {
                case "Modifier":
                    await Navigation.PushAsync(new ModifierEquipe(equipe, lvEquipes));
                    break;
                case "Supprimer":
                    bool res = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cette équipe ?", "Oui", "Non");
                    if (res)
					{
						try
						{
							equipe.Delete();
                            lvEquipes.ItemsSource = new Equipe().GetList();
                        }
						catch (Exception ex)
						{
                            await DisplayAlert("Erreur", ex.Message, "OK");
                            return;
                        }
					}
					break;
                case "Cancel":
                    break;
                case null:
                    break;
            }
        }
    }
}