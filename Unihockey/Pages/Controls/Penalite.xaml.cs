namespace Unihockey.Pages.Controls;

public partial class Penalite : ContentView
{
	public Penalite()
	{
		InitializeComponent();
	}

	private void OncbxPenaliteVisibleChecked(object sender, EventArgs e)
	{ 
		CheckBox cbx = (CheckBox)sender;
		if (cbx.IsChecked)
		{
            // Affiche la penalité
            boxPenalite.IsVisible = true;
        }
        else
		{
            // Cache la penalité
            boxPenalite.IsVisible = false;
        }
	}
}