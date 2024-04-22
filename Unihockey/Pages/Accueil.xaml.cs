namespace Unihockey.Pages;

public partial class Accueil : ContentPage
{
	public Accueil()
	{
		InitializeComponent();
	}


	private void OnbtnCreerMatchClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new GestionMatch());
    }
}