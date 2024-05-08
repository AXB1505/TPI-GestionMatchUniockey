using Unihockey.Model;

namespace Unihockey.Pages;

public partial class GestionEquipes : ContentPage
{
	public GestionEquipes()
	{
		InitializeComponent();
        // D�finition de la source des donn�es de la liste
		lvEquipes.ItemsSource = new Equipe().GetList();
    }

    // M�thode pour la cr�ation d'une nouvelle �quipe
    private void OnbtnCreerClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreerEquipe(lvEquipes));
    }

    // M�thode pour recharger la liste des �quipes
    private void OnbtnRechargerClicked(object sender, EventArgs e)
    {
        lvEquipes.ItemsSource = new Equipe().GetList();
    }

    // M�thode appel�e lorsqu'un �l�ment de la liste est s�lectionn� asynchrone car elle utilise DisplayActionSheet
    private async void OnlvEquipesItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
        // V�rifie si un �l�ment est s�lectionn�
        if (e.SelectedItem != null)
		{
            Equipe equipe = (Equipe)e.SelectedItem;

            // Affiche un menu d'actions pour modifier ou supprimer l'�quipe
			string rep = await DisplayActionSheet("Actions :", "Cancel", null, "Modifier", "Supprimer");
            switch (rep)
            {
                case "Modifier":
                    // Redirection vers la page de modification de l'�quipe
                    await Navigation.PushAsync(new ModifierEquipe(equipe, lvEquipes));
                    break;
                case "Supprimer":
                    // Demande de confirmation pour la suppression de l'�quipe
                    bool res = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cette �quipe ?", "Oui", "Non");
                    if (res)
					{
                        // Tentative de suppression de l'�quipes
						try
						{
							equipe.Delete();
                            lvEquipes.ItemsSource = new Equipe().GetList();
                        }
                        // Affiche un message d'erreur en cas d'�chec
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