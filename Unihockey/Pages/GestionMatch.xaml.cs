using System.ComponentModel.DataAnnotations.Schema;
using Unihockey.Model;

namespace Unihockey.Pages;

public partial class GestionMatch : ContentPage
{
    AffichageMatch affMatch = new AffichageMatch();

    // Instanciation de l'objet Match
    Match mMatch = new Match();

    // Instantiation des points des �quipes
    int iPointsEquipe1;
    int iPointsEquipe2;

    // Instanciation des chronom�tres
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
        // Initialisation du chronom�tre principal selon la dur�e de la p�riode et le nombre de la p�riode re�u en param�tre
        chrPrincipal = new Chronometre(dureePeriode, 0, periode);
        lblChrPrincipal.Text = chrPrincipal.getTempsReset();
    }



    // M�thodes pour les boutons du chronom�tre principal

    // M�thode pour d�marrer le chronom�tre principal
    private void OnbtnPlayClicked(object sender, EventArgs e)
    {
        chrPrincipal.Start();

        // Check si la p�nalit� est en cours et d�marrage du chronom�tre de p�nalit� si n�cessaire pour chaque p�nalit�
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

    // M�thode pour mettre en pause le chronom�tre principal
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

    // M�thode pour le changement de chronom�trage entre croissant et d�croissant
    private void OnCbxCroissantCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        chrPrincipal.setCroissant(cbx.IsChecked);
        lblChrPrincipal.Text = chrPrincipal.GetTempsRestant();
    }





    // M�thodes pour les boutons des p�nalit�s

    // M�thode de d�marrage du chronom�tre de p�nalit� num�ro 1
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
         * Pour de l'am�lioration et l'utilisation d'une seule fonction pour chaque boutons de p�nalit�
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

    // M�thode de pause du chronom�tre de p�nalit� num�ro 1
    private void OnbtnPausePenalite1Clicked(object sender, EventArgs e)
    {
        chrPenalite1.Pause();
    }

    // M�thode de d�marrage du chronom�tre de p�nalit� num�ro 2
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
    // M�thode de pause du chronom�tre de p�nalit� num�ro 2
    private void OnbtnPausePenalite2Clicked(object sender, EventArgs e)
    {
        chrPenalite2.Pause();
    }

    // M�thode de d�marrage du chronom�tre de p�nalit� num�ro 3
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

    // M�thode de pause du chronom�tre de p�nalit� num�ro 3
    private void OnbtnPausePenalite3Clicked(object sender, EventArgs e)
    {
        chrPenalite3.Pause();
    }

    // M�thode de d�marrage du chronom�tre de p�nalit� num�ro 4
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

    // M�thode de pause du chronom�tre de p�nalit� num�ro 4
    private void OnbtnPausePenalite4Clicked(object sender, EventArgs e)
    {
        chrPenalite4.Pause();
    }

    // M�thode de d�marrage du chronom�tre de p�nalit� num�ro 5
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

    //m�thode de pause du chronom�tre de p�nalit� num�ro 5
    private void OnbtnPausePenalite5Clicked(object sender, EventArgs e)
    {
        chrPenalite5.Pause();
    }

    // M�thode de d�marrage du chronom�tre de p�nalit� num�ro 6
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

    // M�thode de pause du chronom�tre de p�nalit� num�ro 6
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
            // Affiche la penalit�
            boxPenalite.IsVisible = true;
            HorizontalStackLayout vslParent1 = (HorizontalStackLayout)cbx.Parent;
            VerticalStackLayout child1 = (VerticalStackLayout)vslParent1.Children.First();
            Border borderPenalite = (Border)child1.Children.First();

            Label lblPenalite = (Label)borderPenalite.Content;

            lblPenalite.Text = chrPenalite1.getTempsReset();
        }
        else
        {
            // Cache la penalit�
            boxPenalite.IsVisible = false;
        }
    }








    // M�thodes pour les boutons des points

    // M�thode pour ajouter un point � l'�quipe 1
    private void OnbtnPointPlusEquipe1Clicked(object sender, EventArgs e)
    {        
        iPointsEquipe1++;
        lblPointEquipe1.Text = $"{iPointsEquipe1}"; 
    }
    
    // M�thode pour enlever un point � l'�quipe 1
    private void OnbtnPointMoinsEquipe1Clicked(object sender, EventArgs e)
    {
        if (iPointsEquipe1 > 0)
        {
            iPointsEquipe1--;
            lblPointEquipe1.Text = $"{iPointsEquipe1}";
        }
    }

    // M�thode pour ajouter un point � l'�quipe 2
    private void OnbtnPointPlusEquipe2Clicked(object sender, EventArgs e)
    {
        iPointsEquipe2++;
        lblPointEquipe2.Text = $"{iPointsEquipe2}";
    }

    // M�thode pour enlever un point � l'�quipe 2
    private void OnbtnPointMoinsEquipe2Clicked(object sender, EventArgs e)
    {
        if (iPointsEquipe2 > 0)
        {
            iPointsEquipe2--;
            lblPointEquipe2.Text = $"{iPointsEquipe2}";
        }
    }









    // Fonction asynchrone pour mettre � jour les labels des chronom�tres
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