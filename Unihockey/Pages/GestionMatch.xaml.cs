using System.ComponentModel.DataAnnotations.Schema;
using Unihockey.Model;

namespace Unihockey.Pages;

public partial class GestionMatch : ContentPage
{
    Chronometre chrPrincipal = new Chronometre();
	public GestionMatch(int periode, int dureePeriode)
	{
        InitializeComponent();
        chrPrincipal = new Chronometre(dureePeriode, 0, periode);
        lblChrPrincipal.Text = chrPrincipal.getTempsReset();
    }

    // M�thodes pour les boutons

    private void OnbtnPlayClicked(object sender, EventArgs e)
    {
        chrPrincipal.Start();
        UpdateLabels();
    }

    private void OnbtnPauseClicked(object sender, EventArgs e)
    {
        chrPrincipal.Pause();
    }

    private void OnCbxCroissantCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        chrPrincipal.setCroissant(cbx.IsChecked);
        lblChrPrincipal.Text = chrPrincipal.GetTempsRestant();
    }


    private void OncbxPenaliteVisibleChecked(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        HorizontalStackLayout hslParent = (HorizontalStackLayout)cbx.Parent;
        VerticalStackLayout boxPenalite = (VerticalStackLayout)hslParent.Children.First();

        if (cbx.IsChecked)
        {
            // Affiche la penalit�
            boxPenalite.IsVisible = true;
        }
        else
        {
            // Cache la penalit�
            boxPenalite.IsVisible = false;
        }
    }

    private async Task UpdateLabels()
    {
        while (true)
        {
            lblChrPrincipal.Text = chrPrincipal.GetTempsRestant();
            await Task.Delay(25);
        }
    }
}