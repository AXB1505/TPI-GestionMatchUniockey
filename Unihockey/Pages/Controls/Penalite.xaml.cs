using Unihockey.Model;

namespace Unihockey.Pages.Controls;

public partial class Penalite : ContentView
{
	Chronometre chrono = new Chronometre();
	public Penalite()
	{
		InitializeComponent();
	}

	private void OncbxPenaliteVisibleChecked(object sender, EventArgs e)
	{ 
		CheckBox cbx = (CheckBox)sender;
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
}