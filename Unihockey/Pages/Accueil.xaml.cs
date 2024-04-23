using Unihockey.Model;

namespace Unihockey.Pages;

public partial class Accueil : ContentPage
{
	public Accueil()
	{
		InitializeComponent();
	}


	private void OnbtnCreerMatch1x24Clicked(object sender, EventArgs e)
	{
		int iPeriode = 1;
		int iDureePeriode = 24;
		Navigation.PushAsync(new GestionMatch(iPeriode, iDureePeriode));
    }

    private void OnbtnCreerMatch2x20Clicked(object sender, EventArgs e)
    {
        int iPeriode = 2;
        int iDureePeriode = 20;
        Match match = new Match();
        Navigation.PushAsync(new GestionMatch(iPeriode, iDureePeriode));
    }
}