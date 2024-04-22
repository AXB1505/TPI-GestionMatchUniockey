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
		Match match = new Match();
		Navigation.PushAsync(new GestionMatch());
    }

    private void OnbtnCreerMatch2x20Clicked(object sender, EventArgs e)
    {
        Match match = new Match();
        Navigation.PushAsync(new GestionMatch());
    }
}