using Unihockey.Model;

namespace Unihockey.Pages;

public partial class ModifierEquipe : ContentPage
{
    // Instanciation de léquipe à modifier
	Equipe equEquipe = new Equipe();

    // Instanciation de la ListView pour la mise à jour de la liste des équipes de la page précédente
    ListView lvListView = new ListView();

	public ModifierEquipe(object equipe, ListView listView)
	{
        InitializeComponent();

        // Récupération de l'équipe à modifier et de la ListView
        equEquipe = (Equipe)equipe;
        lvListView = listView;

        // Remplissage des champs de la page en fonction de l'équipe à modifier
        pickCategorie.ItemsSource = new Categorie().GetList();
        pickCategorie.SelectedIndex = equEquipe.getCategorie().getId()-1;
        lblEquipeAModifier.Text = equEquipe.getNom();
        tbxNom.Text = equEquipe.getNom();
    }

    // Méthode appelée par le bouton de confirmation du formulaire pour la modification de l'équipe
    private void OnbtnConfirmerClicked(object sender, EventArgs e)
    {
        // Vérification Si la saisie des champs a été effectuée
        if(tbxNom.Text != null || pickCategorie.SelectedItem != null)
        {
            // Création d'une nouvelle équipe avec les informations saisies
            Equipe equNouvelleEquipe = new Equipe(tbxNom.Text, (Categorie)pickCategorie.SelectedItem);

            // Tentative de modification de l'équipe
            try
            {
                equEquipe.Edit(equNouvelleEquipe);
            }
            // Envoie d'un message d'erreur si l'équipe existe déjà
            catch
            {
                DisplayAlert("Erreur de saisie", $"Il existe déjà une entrée {equNouvelleEquipe}", "OK");
                return;
            }

            // Rafraichissement de la liste des équipes dans la page précédente et retour à celle-ci
            lvListView.ItemsSource = new Equipe().GetList();
            Navigation.PopAsync();
        }
        // Sinon, si la saisie est incorrecte, envoie d'un message d'erreur
        else
        {
            DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            return;
        }
    }
}