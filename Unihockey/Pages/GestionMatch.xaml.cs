using System.ComponentModel.DataAnnotations.Schema;
using Unihockey.Model;

namespace Unihockey.Pages;

public partial class GestionMatch : ContentPage
{
    AffichageMatch affMatch = new AffichageMatch();

    // Instanciation de l'objet Match
    Match mMatch = new Match();

    // Instantiation des points des équipes
    int iPointsEquipe1;
    int iPointsEquipe2;

    // Instanciation des chronomètres
    Chronometre chrPrincipal = new Chronometre();

    Chronometre chrPenalite1 = new Chronometre(0, 30, 1);
    Chronometre chrPenalite2 = new Chronometre(0, 30, 1);
    Chronometre chrPenalite3 = new Chronometre(0, 30, 1);
    Chronometre chrPenalite4 = new Chronometre(0, 30, 1);
    Chronometre chrPenalite5 = new Chronometre(0, 30, 1);
    Chronometre chrPenalite6 = new Chronometre(0, 30, 1);

    public GestionMatch(int periode, int dureePeriode)
	{
        InitializeComponent();
        // Initialisation du chronomètre principal selon la durée de la période et le nombre de la période reçu en paramètre
        chrPrincipal = new Chronometre(dureePeriode, 0, periode);
        lblChrPrincipal.Text = chrPrincipal.getTempsReset();
    }



    // Méthodes pour les boutons du chronomètre principal

    // Méthode pour démarrer le chronomètre principal
    private void OnbtnPlayClicked(object sender, EventArgs e)
    {
        chrPrincipal.Start();

        // Check si la pénalité est en cours et démarrage du chronomètre de pénalité si nécessaire pour chaque pénalité
        if(chrPenalite1.getTempsReset() != chrPenalite1.GetTempsRestant())
        {
            chrPenalite1.Start();
        }
        if (chrPenalite2.getTempsReset() != chrPenalite2.GetTempsRestant())
        {
            chrPenalite2.Start();
        }
        if (chrPenalite3.getTempsReset() != chrPenalite3.GetTempsRestant())
        {
            chrPenalite3.Start();
        }
        if (chrPenalite4.getTempsReset() != chrPenalite4.GetTempsRestant())
        {
            chrPenalite4.Start();
        }
        if (chrPenalite5.getTempsReset() != chrPenalite5.GetTempsRestant())
        {
            chrPenalite5.Start();
        }
        if (chrPenalite6.getTempsReset() != chrPenalite6.GetTempsRestant())
        {
            chrPenalite6.Start();
        }

        UpdateLabels();
    }

    // Méthode pour mettre en pause le chronomètre principal
    private void OnbtnPauseClicked(object sender, EventArgs e)
    {
        chrPrincipal.Pause();

        chrPenalite1.Pause();
        chrPenalite2.Pause();
        chrPenalite3.Pause();
        chrPenalite4.Pause();
        chrPenalite5.Pause();
        chrPenalite6.Pause();
    }

    // Méthode pour le changement de chronomètrage entre croissant et décroissant
    private void OnCbxCroissantCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        chrPrincipal.setCroissant(cbx.IsChecked);
        lblChrPrincipal.Text = chrPrincipal.GetTempsRestant();
    }





    // Méthodes pour les boutons des pénalités

    // Méthode de démarrage du chronomètre de pénalité numéro 1
    private void OnbtnStartPenalite1Clicked(object sender, EventArgs e)
    {
        if (chrPrincipal.getStatus())
        {
            if(chrPenalite1.getStatus() == false)
            {
                chrPenalite1.Start();
            }
        }

        /*
         * Pour de l'amélioration et l'utilisation d'une seule fonction pour chaque boutons de pénalité
         * 
        Button btn = (Button)sender;
        HorizontalStackLayout hslParent1 = (HorizontalStackLayout)btn.Parent;
        VerticalStackLayout vslParent2 = (VerticalStackLayout)hslParent1.Parent;
        Border borderPenalite = (Border)vslParent2.Children.First();

        Label lblPenalite = (Label)borderPenalite.Content;

        int iNumeroPenalite = int.Parse(lblPenalite.Text);

        switch (iNumeroPenalite)
        {
            case 1:
                chrPenalite1.Start();
                break;
            case 2:
                chrPenalite2.Start();
                break;
            case 3:
                chrPenalite3.Start();
                break;
            case 4:
                chrPenalite4.Start();
                break;
            case 5:
                chrPenalite5.Start();
                break;
            case 6:
                chrPenalite6.Start();
                break;
        }
        */
    }

    // Méthode de pause du chronomètre de pénalité numéro 1
    private void OnbtnPausePenalite1Clicked(object sender, EventArgs e)
    {
        chrPenalite1.Pause();
    }

    // Méthode de démarrage du chronomètre de pénalité numéro 2
    private void OnbtnStartPenalite2Clicked(object sender, EventArgs e)
    {
        if (chrPrincipal.getStatus())
        {
            if (chrPenalite2.getStatus() == false)
            {
                chrPenalite2.Start();
            }
        }
    }
    // Méthode de pause du chronomètre de pénalité numéro 2
    private void OnbtnPausePenalite2Clicked(object sender, EventArgs e)
    {
        chrPenalite2.Pause();
    }

    // Méthode de démarrage du chronomètre de pénalité numéro 3
    private void OnbtnStartPenalite3Clicked(object sender, EventArgs e)
    {
        if (chrPrincipal.getStatus())
        {
            if (chrPenalite3.getStatus() == false)
            {
                chrPenalite3.Start();
            }
        }
    }

    // Méthode de pause du chronomètre de pénalité numéro 3
    private void OnbtnPausePenalite3Clicked(object sender, EventArgs e)
    {
        chrPenalite3.Pause();
    }

    // Méthode de démarrage du chronomètre de pénalité numéro 4
    private void OnbtnStartPenalite4Clicked(object sender, EventArgs e)
    {
        if (chrPrincipal.getStatus())
        {
            if (chrPenalite4.getStatus() == false)
            {
                chrPenalite4.Start();
            }
        }
    }

    // Méthode de pause du chronomètre de pénalité numéro 4
    private void OnbtnPausePenalite4Clicked(object sender, EventArgs e)
    {
        chrPenalite4.Pause();
    }

    // Méthode de démarrage du chronomètre de pénalité numéro 5
    private void OnbtnStartPenalite5Clicked(object sender, EventArgs e)
    {
        if (chrPrincipal.getStatus())
        {
            if (chrPenalite5.getStatus() == false)
            {
                chrPenalite5.Start();
            }
        }
    }

    //méthode de pause du chronomètre de pénalité numéro 5
    private void OnbtnPausePenalite5Clicked(object sender, EventArgs e)
    {
        chrPenalite5.Pause();
    }

    // Méthode de démarrage du chronomètre de pénalité numéro 6
    private void OnbtnStartPenalite6Clicked(object sender, EventArgs e)
    {
        if (chrPrincipal.getStatus())
        {
            if (chrPenalite6.getStatus() == false)
            {
                chrPenalite6.Start();
            }
        }
    }

    // Méthode de pause du chronomètre de pénalité numéro 6
    private void OnbtnPausePenalite6Clicked(object sender, EventArgs e)
    {
        chrPenalite6.Pause();
    }
    private void OncbxPenaliteVisibleChecked(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        HorizontalStackLayout hslParent = (HorizontalStackLayout)cbx.Parent;
        VerticalStackLayout boxPenalite = (VerticalStackLayout)hslParent.Children.First();

        if (cbx.IsChecked)
        {
            // Affiche la penalité
            boxPenalite.IsVisible = true;
            HorizontalStackLayout vslParent1 = (HorizontalStackLayout)cbx.Parent;
            VerticalStackLayout child1 = (VerticalStackLayout)vslParent1.Children.First();
            Border borderPenalite = (Border)child1.Children.First();

            Label lblPenalite = (Label)borderPenalite.Content;

            lblPenalite.Text = chrPenalite1.getTempsReset();
        }
        else
        {
            // Cache la penalité
            boxPenalite.IsVisible = false;
        }
    }








    // Méthodes pour les boutons des points

    // Méthode pour ajouter un point à l'équipe 1
    private void OnbtnPointPlusEquipe1Clicked(object sender, EventArgs e)
    {        
        iPointsEquipe1++;
        lblPointEquipe1.Text = $"{iPointsEquipe1}"; 
    }
    
    // Méthode pour enlever un point à l'équipe 1
    private void OnbtnPointMoinsEquipe1Clicked(object sender, EventArgs e)
    {
        if (iPointsEquipe1 > 0)
        {
            iPointsEquipe1--;
            lblPointEquipe1.Text = $"{iPointsEquipe1}";
        }
    }

    // Méthode pour ajouter un point à l'équipe 2
    private void OnbtnPointPlusEquipe2Clicked(object sender, EventArgs e)
    {
        iPointsEquipe2++;
        lblPointEquipe2.Text = $"{iPointsEquipe2}";
    }

    // Méthode pour enlever un point à l'équipe 2
    private void OnbtnPointMoinsEquipe2Clicked(object sender, EventArgs e)
    {
        if (iPointsEquipe2 > 0)
        {
            iPointsEquipe2--;
            lblPointEquipe2.Text = $"{iPointsEquipe2}";
        }
    }









    // Fonction asynchrone pour mettre à jour les labels des chronomètres
    private async Task UpdateLabels()
    {
        while (true)
        {
            lblChrPrincipal.Text = chrPrincipal.GetTempsRestant();
            lblPenalite1.Text = chrPenalite1.GetTempsRestant();
            lblPenalite2.Text = chrPenalite2.GetTempsRestant();
            lblPenalite3.Text = chrPenalite3.GetTempsRestant();
            lblPenalite4.Text = chrPenalite4.GetTempsRestant();
            lblPenalite5.Text = chrPenalite5.GetTempsRestant();
            lblPenalite6.Text = chrPenalite6.GetTempsRestant();

            affMatch.Layout.lblChronoPrincipal.Text = chrPrincipal.GetTempsRestant();
            await Task.Delay(25);
        }
    }
}