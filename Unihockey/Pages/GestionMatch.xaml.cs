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
    private Match mMatch = new Match();

    // Variables de contrôle que le match est terminé
    private bool bMatchFini = false;

    // Instantiation des points des équipes
    private int iPointsEquipe1;
    private int iPointsEquipe2;

    // Instanciation des chronomètres
    private Chronometre chrPrincipal = new Chronometre();
    private Chronometre chrTempsMort = new Chronometre(0,30,1);
    private Chronometre chrPenalite1 = new Chronometre();
    private Chronometre chrPenalite2 = new Chronometre();
    private Chronometre chrPenalite3 = new Chronometre();
    private Chronometre chrPenalite4 = new Chronometre();
    private Chronometre chrPenalite5 = new Chronometre();
    private Chronometre chrPenalite6 = new Chronometre();

    // Instanciaition de la liste d'équipe
    List<Equipe> listEquipes = new List<Equipe>(new Equipe().GetList());

    // Instanciation des listes de labels et de checkboxs pour le passage sur la fenêtre d'affichage du match
    private List<Label> labels = new List<Label>();
    private List<CheckBox> checkboxs = new List<CheckBox>();

    public GestionMatch(int periode, int dureePeriode)
	{
        InitializeComponent();
        // Initialisation du chronomètre principal selon la durée de la période et le nombre de la période reçu en paramètre
        chrPrincipal = new Chronometre(dureePeriode, 0, periode);
        lblChrPrincipal.Text = chrPrincipal.getTempsReset();

        // Ajout des labels dans les listes de labels pour le passage sur la fenêtre d'affichage du match
        labels.Add(lblChrPrincipal);
        labels.Add(lblPenalite1);
        labels.Add(lblPenalite2);
        labels.Add(lblPenalite3);
        labels.Add(lblPenalite4);
        labels.Add(lblPenalite5);
        labels.Add(lblPenalite6);
        labels.Add(lblPointEquipe1);
        labels.Add(lblPointEquipe2);
        labels.Add(lblChrTempsMort);

        // Ajout des checkboxs dans la liste de checkboxs pour le passage sur la fenêtre d'affichage du match
        checkboxs.Add(cbxPenalite1);
        checkboxs.Add(cbxPenalite2);
        checkboxs.Add(cbxPenalite3);
        checkboxs.Add(cbxPenalite4);
        checkboxs.Add(cbxPenalite5);
        checkboxs.Add(cbxPenalite6);

        // Lancement de la fonction asynchrone pour la mise à jour des labels des chronomètres
        UpdateLabels();

        // Remplissage des listes déroulantes des équipes avec les équipes de la base de données
        pickEquipe1.ItemsSource = listEquipes;
        pickEquipe2.ItemsSource = listEquipes;
    }



    // Méthodes pour les boutons du chronomètre principal

    // Méthode pour démarrer le chronomètre principal
    private void OnbtnPlayClicked(object sender, EventArgs e)
    {
        if (chrPrincipal.getEstFini() == false)
        {
            chrPrincipal.Start();
            chrTempsMort.Pause();
            // Check si la pénalité est en cours et démarrage du chronomètre de pénalité si nécessaire pour chaque pénalité
            if (chrPenalite1.getTempsReset() != chrPenalite1.GetTempsRestant())
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
        }
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
            mMatch = new Match();

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
        // Ouverture de la fenêtre d'affichage du match et transmission des labels et checkboxs pour l'affichage
        Window affichageMatch = new Window(new AffichageMatch(labels, checkboxs));
        Application.Current.OpenWindow(affichageMatch);
    }


    private void OnbtnPlayTempsMortClicked(object sender, EventArgs e)
    {
        chrTempsMort.Start();
        chrPrincipal.Pause();
        chrPenalite1.Pause();
        chrPenalite2.Pause();
        chrPenalite3.Pause();
        chrPenalite4.Pause();
        chrPenalite5.Pause();
        chrPenalite6.Pause();
    }

    private void OnbtnPauseTempsMortClicked(object sender, EventArgs e)
    {
        chrTempsMort.Pause();
    }



    // Méthodes pour les boutons des pénalités

    // Méthode de démarrage du chronomètre de pénalité numéro 1
    private void OnbtnStartPenalite1Clicked(object sender, EventArgs e)
    {
        if (chrPrincipal.getStatus())
        {
            chrPenalite1.Start();
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
            chrPenalite2.Start();
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
            chrPenalite3.Start();
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
            chrPenalite4.Start();
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
            chrPenalite5.Start();
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
            chrPenalite6.Start();
        }
    }

    // Méthode de pause du chronomètre de pénalité numéro 6
    private void OnbtnPausePenalite6Clicked(object sender, EventArgs e)
    {
        chrPenalite6.Pause();
    }


    // Méthode pour la vérification du choix des équipes (ne peuvent pas être les mêmes)
    private void OnpickEquipeSelectedItemChanged(object sender, EventArgs e)
    {
        Picker pick = (Picker)sender;
        if (pickEquipe1.SelectedItem == pickEquipe2.SelectedItem)
        {
            // Affichage d'un message d'erreur
            DisplayAlert("Erreur", "Vous ne pouvez pas choisir la même équipe pour les deux équipes.", "OK");
            // Remise à zéro de la liste déroulante
            pick.SelectedItem = null;
        }
    }



    // Méthodes pour afficher ou caché les pénalités

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
                    cbx.IsChecked = true;
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
                    boxPenalite2.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = true;
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
                    boxPenalite3.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = true;
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
                    boxPenalite4.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = true;
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
                    boxPenalite5.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = true;
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
                    boxPenalite5.IsVisible = false;
                    break;
                case null:
                    cbx.IsChecked = true;
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
        // Vérification si le score est supérieur à 0 car on ne peut pas avoir de score négatif
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
        // Vérification si le score est supérieur à 0 car on ne peut pas avoir de score négatif
        if (iPointsEquipe2 > 0)
        {
            iPointsEquipe2--;
            lblPointEquipe2.Text = $"{iPointsEquipe2}";
        }
    }









    // Méthode asynchrone pour la mise à jour des labels des chronomètres
    private async Task UpdateLabels()
    {
        while (true)
        {
            lblChrPrincipal.Text = chrPrincipal.GetTempsRestant();
            lblChrTempsMort.Text = chrTempsMort.GetTempsRestant();
            lblPenalite1.Text = chrPenalite1.GetTempsRestant();
            lblPenalite2.Text = chrPenalite2.GetTempsRestant();
            lblPenalite3.Text = chrPenalite3.GetTempsRestant();
            lblPenalite4.Text = chrPenalite4.GetTempsRestant();
            lblPenalite5.Text = chrPenalite5.GetTempsRestant();
            lblPenalite6.Text = chrPenalite6.GetTempsRestant();

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