using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class CoinView : ContentPage
{
	public CoinView()
	{
		InitializeComponent();
		this.BindingContext = new CoinViewModel();
	}
}