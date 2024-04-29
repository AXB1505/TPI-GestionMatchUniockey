namespace Unihockey.Pages;

public partial class AffichageMatch : ContentPage
{
    // Instanciation des listes de labels et de checkbox pour les récupérer depuis la page de gestion du match
    List<Label> labelsParent = new List<Label>();
    List<CheckBox> checkBoxesParent = new List<CheckBox>();

	public AffichageMatch(List<Label> labels, List<CheckBox> checkBoxes)
	{
		InitializeComponent();
        // Récupération des listes de labels et de checkbox depuis la page de gestion du match
        labelsParent = labels;
        checkBoxesParent = checkBoxes;
        UpdateLabels();
	}

    // Boucle pour mettre à jour les chronomètres depuis la page de gestion du match
    private async Task UpdateLabels()
    {
        while (true)
        {
            // Mise à jour des labels de chronomètres
            lblChrPrincipal.Text = labelsParent[0].Text;
            lblPenalite1.Text = labelsParent[1].Text;
            lblPenalite2.Text = labelsParent[2].Text;
            lblPenalite3.Text = labelsParent[3].Text;
            lblPenalite4.Text = labelsParent[4].Text;
            lblPenalite5.Text = labelsParent[5].Text;
            lblPenalite6.Text = labelsParent[6].Text;
            lblPointEquipe1.Text = labelsParent[7].Text;
            lblPointEquipe2.Text = labelsParent[8].Text;

            // Check de l'affichage des pénalités en fonction des checkbox de la page de gestion du match
            if (checkBoxesParent[0].IsChecked)
            {
                boxPenalite1.IsVisible = true;
            }
            else
            {
                boxPenalite1.IsVisible = false;
            }
            if (checkBoxesParent[1].IsChecked)
            {
                boxPenalite2.IsVisible = true;
            }
            else
            {
                boxPenalite2.IsVisible = false;
            }
            if (checkBoxesParent[2].IsChecked)
            {
                boxPenalite3.IsVisible = true;
            }
            else
            {
                boxPenalite3.IsVisible = false;
            }
            if (checkBoxesParent[3].IsChecked)
            {
                boxPenalite4.IsVisible = true;
            }
            else
            {
                boxPenalite4.IsVisible = false;
            }
            if (checkBoxesParent[4].IsChecked)
            {
                boxPenalite5.IsVisible = true;
            }
            else
            {
                boxPenalite5.IsVisible = false;
            }
            if (checkBoxesParent[5].IsChecked)
            {
                boxPenalite6.IsVisible = true;
            }
            else
            {
                boxPenalite6.IsVisible = false;
            }


            await Task.Delay(25);
        }
    }
}