using Unihockey.Model;

namespace Unihockey.Pages;

public partial class GestionEquipes : ContentPage
{
	public GestionEquipes()
	{
		InitializeComponent();
        // Définition de la source des données de la liste
		lvEquipes.ItemsSource = new Equipe().GetList();
    }

    // Méthode pour la création d'une nouvelle équipe
    private void OnbtnCreerClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreerEquipe(lvEquipes));
    }

    // Méthode pour recharger la liste des équipes
    private void OnbtnRechargerClicked(object sender, EventArgs e)
    {
        lvEquipes.ItemsSource = new Equipe().GetList();
    }

    // Méthode appelée lorsqu'un élément de la liste est sélectionné asynchrone car elle utilise DisplayActionSheet
    private async void OnlvEquipesItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
        // Vérifie si un élément est sélectionné
        if (e.SelectedItem != null)
		{
            Equipe equipe = (Equipe)e.SelectedItem;

            // Affiche un menu d'actions pour modifier ou supprimer l'équipe
			string rep = await DisplayActionSheet("Actions :", "Cancel", null, "Modifier", "Supprimer");
            switch (rep)
            {
                case "Modifier":
                    // Redirection vers la page de modification de l'équipe
                    await Navigation.PushAsync(new ModifierEquipe(equipe, lvEquipes));
                    break;
                case "Supprimer":
                    // Demande de confirmation pour la suppression de l'équipe
                    bool res = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cette équipe ?", "Oui", "Non");
                    if (res)
					{
                        // Tentative de suppression de l'équipes
						try
						{
							equipe.Delete();
                            lvEquipes.ItemsSource = new Equipe().GetList();
                        }
                        // Affiche un message d'erreur en cas d'échec
						catch (Exception ex)
						{
                            await DisplayAlert("Erreur", ex.Message, "OK");
                            lvEquipes.SelectedItem = null;
                            return;
                        }
					}
					break;
            }
            lvEquipes.SelectedItem = null;
        }
    }
}