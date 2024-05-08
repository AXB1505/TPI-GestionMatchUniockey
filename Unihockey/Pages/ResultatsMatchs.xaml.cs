using Unihockey.Model;

namespace Unihockey.Pages;

public partial class ResultatsMatchs : ContentPage
{

	public ResultatsMatchs()
	{
		InitializeComponent();

		lvMatchs.ItemsSource = new Match().getList();
	}

	private void OnbtnRechargerClicked(object sender, EventArgs e)
	{
		lvMatchs.ItemsSource = new Match().getList();
    }
}