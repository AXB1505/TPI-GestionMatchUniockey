using Microsoft.Maui.Platform;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Threading.Channels;
using Unihockey.Model;

namespace Unihockey.Pages;

public partial class GestionMatch : ContentPage
{
    // Instanciation de l'objet Match
    Match mMatch = new Match();

    // Instantiation des points des équipes
    int iPointsEquipe1;
    int iPointsEquipe2;

    // Instanciation des chronomètres
    Chronometre chrPrincipal = new Chronometre();

    Chronometre chrPenalite1 = new Chronometre();
    Chronometre chrPenalite2 = new Chronometre();
    Chronometre chrPenalite3 = new Chronometre();
    Chronometre chrPenalite4 = new Chronometre();
    Chronometre chrPenalite5 = new Chronometre();
    Chronometre chrPenalite6 = new Chronometre();

    // Instanciation de la fenêtre d'affichage du match
    Window affichageMatch = new Window(new AffichageMatch());

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

    private async void OnbtnStopClicked(object sender, EventArgs e)
    {
        // Demande de confirmation pour l'arrêt du match et vérification de la réponse
        bool rep = await DisplayAlert("Arrêt du Match", "Êtes-vous sûr de vouloir arrêter le match ?\n" +
            "Ceci entrainera l'enregistrement des résultats et vous ne pourez plus retourner en arrière.", "Oui", "Non");
        if (rep)
        {
            // Enregistrement des résultats
                // A faire

            // Redirection sur une nouvelle page de gestion de match pour un nouveau match (demandé par le client)
            await Navigation.PushAsync(new GestionMatch(chrPrincipal.getNombrePeriode(), chrPrincipal.getMinutesPeriode()));
            // Supression de la page active de gestion du match
            Navigation.RemovePage(this);
        }
    }

    // Méthode pour le changement de chronomètrage entre croissant et décroissant
    private void OnCbxCroissantCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        chrPrincipal.setCroissant(cbx.IsChecked);
        lblChrPrincipal.Text = chrPrincipal.GetTempsRestant();
    }

    private void OnbtnAffichageSecondEcranClicked(object sender, EventArgs e)
    {
        Application.Current.OpenWindow(affichageMatch);
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


    // Méthodes pour afficher ou caché les pénalités (semblables)
    // Méthode pour afficher ou cacher la pénalité 1 asynchrone pour le choix de la durée de la pénalité
    private async void OncbxPenalite1VisibleChecked(object sender, EventArgs e)
    {
        // Récupération de la checkbox
        CheckBox cbx = (CheckBox)sender;

        // Check si la checkbox est cochée
        if (cbx.IsChecked)
        {
            // Affiche la penalité
            boxPenalite1.IsVisible = true;
            // Demande de choix de la durée de la pénalité
            string rep = await DisplayActionSheet("Durée de la pénalité :", "Cancel", null, "2min", "5min", "10min");
            // Switch pour le choix de la durée de la pénalité
            switch (rep)
            {
                case "2min":
                    chrPenalite1 = new Chronometre(2, 0, 1);
                    break;
                case "5min":
                    chrPenalite1 = new Chronometre(5, 0, 1);
                    break;
                case "10min":
                    chrPenalite1 = new Chronometre(10, 0, 1);
                    break;
                case "Cancel":
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
            }
            lblPenalite1.Text = chrPenalite1.GetTempsRestant();
        }
        else
        {
            // Cache la penalité
            boxPenalite1.IsVisible = false;
        }
    }

    // Méthode pour afficher ou cacher la pénalité 2
    private async void OncbxPenalite2VisibleChecked(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        if (cbx.IsChecked)
        {
            // Affiche la penalité
            boxPenalite2.IsVisible = true;
            string rep = await DisplayActionSheet("Durée de la pénalité :", "Cancel", null, "2min", "5min", "10min");
            switch (rep)
            {
                case "2min":
                    chrPenalite2 = new Chronometre(2, 0, 1);
                    break;
                case "5min":
                    chrPenalite2 = new Chronometre(5, 0, 1);
                    break;
                case "10min":
                    chrPenalite2 = new Chronometre(10, 0, 1);
                    break;
                case "Cancel":
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
            }
            lblPenalite2.Text = chrPenalite2.GetTempsRestant();
        }
        else
        {
            // Cache la penalité
            boxPenalite2.IsVisible = false;
        }
    }

    // Méthode pour afficher ou cacher la pénalité 3
    private async void OncbxPenalite3VisibleChecked(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        if (cbx.IsChecked)
        {
            // Affiche la penalité
            boxPenalite3.IsVisible = true;
            string rep = await DisplayActionSheet("Durée de la pénalité :", "Cancel", null, "2min", "5min", "10min");
            switch (rep)
            {
                case "2min":
                    chrPenalite3 = new Chronometre(2, 0, 1);
                    break;
                case "5min":
                    chrPenalite3 = new Chronometre(5, 0, 1);
                    break;
                case "10min":
                    chrPenalite3 = new Chronometre(10, 0, 1);
                    break;
                case "Cancel":
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
            }
            lblPenalite3.Text = chrPenalite3.GetTempsRestant();
        }
        else
        {
            // Cache la penalité
            boxPenalite3.IsVisible = false;
        }   
    }

    // Méthode pour afficher ou cacher la pénalité 4
    private async void OncbxPenalite4VisibleChecked(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        if (cbx.IsChecked)
        {
            // Affiche la penalité
            boxPenalite4.IsVisible = true;
            string rep = await DisplayActionSheet("Durée de la pénalité :", "Cancel", null, "2min", "5min", "10min");
            switch (rep)
            {
                case "2min":
                    chrPenalite4 = new Chronometre(2, 0, 1);
                    break;
                case "5min":
                    chrPenalite4 = new Chronometre(5, 0, 1);
                    break;
                case "10min":
                    chrPenalite4 = new Chronometre(10, 0, 1);
                    break;
                case "Cancel":
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
            }
            lblPenalite4.Text = chrPenalite4.GetTempsRestant();
        }
        else
        {
            // Cache la penalité
            boxPenalite4.IsVisible = false;
        }
    }

    // Méthode pour afficher ou cacher la pénalité 5
    private async void OncbxPenalite5VisibleChecked(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        if (cbx.IsChecked)
        {
            // Affiche la penalité
            boxPenalite5.IsVisible = true;
            string rep = await DisplayActionSheet("Durée de la pénalité :", "Cancel", null, "2min", "5min", "10min");
            switch (rep)
            {
                case "2min":
                    chrPenalite5 = new Chronometre(2, 0, 1);
                    break;
                case "5min":
                    chrPenalite5 = new Chronometre(5, 0, 1);
                    break;
                case "10min":
                    chrPenalite5 = new Chronometre(10, 0, 1);
                    break;
                case "Cancel":
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
            }
            lblPenalite5.Text = chrPenalite5.GetTempsRestant();
        }
        else
        {
            // Cache la penalité
            boxPenalite5.IsVisible = false;
        }
    }

    // Méthode pour afficher ou cacher la pénalité 6
    private async void OncbxPenalite6VisibleChecked(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        if (cbx.IsChecked)
        {
            // Affiche la penalité
            boxPenalite6.IsVisible = true;
            string rep = await DisplayActionSheet("Durée de la pénalité :", "Cancel", null, "2min", "5min", "10min");
            switch (rep)
            {
                case "2min":
                    chrPenalite6 = new Chronometre(2, 0, 1);
                    break;
                case "5min":
                    chrPenalite6 = new Chronometre(5, 0, 1);
                    break;
                case "10min":
                    chrPenalite6 = new Chronometre(10, 0, 1);
                    break;
                case "Cancel":
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = false;
                    boxPenalite1.IsVisible = false;
                    break;
            }
            lblPenalite6.Text = chrPenalite6.GetTempsRestant();
        }
        else
        {
            // Cache la penalité
            boxPenalite6.IsVisible = false;
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

            /*
            affichageMatch.FindByName<Label>("lblChrPrincipal").Text = chrPrincipal.GetTempsRestant();
            affichageMatch.FindByName<Label>("lblPenalite1").Text = chrPenalite1.GetTempsRestant();
            affichageMatch.FindByName<Label>("lblPenalite2").Text = chrPenalite2.GetTempsRestant();
            affichageMatch.FindByName<Label>("lblPenalite3").Text = chrPenalite3.GetTempsRestant();
            affichageMatch.FindByName<Label>("lblPenalite4").Text = chrPenalite4.GetTempsRestant();
            affichageMatch.FindByName<Label>("lblPenalite5").Text = chrPenalite5.GetTempsRestant();
            affichageMatch.FindByName<Label>("lblPenalite6").Text = chrPenalite6.GetTempsRestant();
            */
            await Task.Delay(25);
        }
    }






    /*
     * Amélioration possible pour l'affichage des pénalités avec une méthode générique
     * 
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
    */


    /*
     * Amélioration possible pour l'affichage des pénalités avec une méthode générique
     *
    private async void ChoixDureePenalite(CheckBox cbxPenlite, Chronometre chronometrePenalite, Label labelPenalite, VerticalStackLayout boxPenalite)
    {
        if (cbxPenlite.IsChecked)
        {
            // Affiche la penalité
            boxPenalite.IsVisible = true;
            // Demande de choix de la durée de la pénalité
            string rep = await DisplayActionSheet("Veuillez choisir la durée de la pénalité", "Cancel", null, "2min", "5min", "10min");
            // Switch pour le choix de la durée de la pénalité
            switch (rep)
            {
                case "2min":
                    chronometrePenalite = new Chronometre(2, 0, 1);
                    break;
                case "5min":
                    chronometrePenalite = new Chronometre(5, 0, 1);
                    break;
                case "10min":
                    chronometrePenalite = new Chronometre(10, 0, 1);
                    break;
                case "Cancel":
                    cbxPenlite.IsChecked = false;
                    chronometrePenalite = new Chronometre(10, 0, 1);
                    break;
            }
            labelPenalite.Text = chronometrePenalite.GetTempsRestant();
        }
        else
        {
            // Cache la penalité
            boxPenalite.IsVisible = false;
        }
    }
    */
}