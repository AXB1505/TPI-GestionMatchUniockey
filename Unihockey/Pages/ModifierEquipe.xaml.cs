using Unihockey.Model;

namespace Unihockey.Pages;

public partial class ModifierEquipe : ContentPage
{
    // Instanciation de l�quipe � modifier
	Equipe equEquipe = new Equipe();

    // Instanciation de la ListView pour la mise � jour de la liste des �quipes de la page pr�c�dente
    ListView lvListView = new ListView();

	public ModifierEquipe(object equipe, ListView listView)
	{
        InitializeComponent();

        // R�cup�ration de l'�quipe � modifier et de la ListView
        equEquipe = (Equipe)equipe;
        lvListView = listView;

        // Remplissage des champs de la page en fonction de l'�quipe � modifier
        pickCategorie.ItemsSource = new Categorie().GetList();
        pickCategorie.SelectedIndex = equEquipe.getCategorie().getId()-1;
        lblEquipeAModifier.Text = equEquipe.getNom();
        tbxNom.Text = equEquipe.getNom();
    }

    // M�thode appel�e par le bouton de confirmation du formulaire pour la modification de l'�quipe
    private void OnbtnConfirmerClicked(object sender, EventArgs e)
    {
        // V�rification Si la saisie des champs a �t� effectu�e
        if(tbxNom.Text != null || pickCategorie.SelectedItem != null)
        {
            // Cr�ation d'une nouvelle �quipe avec les informations saisies
            Equipe equNouvelleEquipe = new Equipe(tbxNom.Text, (Categorie)pickCategorie.SelectedItem);

            // Tentative de modification de l'�quipe
            try
            {
                equEquipe.Edit(equNouvelleEquipe);
            }
            // Envoie d'un message d'erreur si l'�quipe existe d�j�
            catch
            {
                DisplayAlert("Erreur de saisie", $"Il existe d�j� une entr�e {equNouvelleEquipe}", "OK");
                return;
            }

            // Rafraichissement de la liste des �quipes dans la page pr�c�dente et retour � celle-ci
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