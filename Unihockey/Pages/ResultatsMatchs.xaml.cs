using Unihockey.Model;

namespace Unihockey.Pages;

public partial class ResultatsMatchs : ContentPage
{
	List<Match> listMatch = new Match().getList();

	public ResultatsMatchs()
	{
		InitializeComponent();

		lvMatchs.ItemsSource = listMatch;
	}
}