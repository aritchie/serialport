using System;
using Xamarin.Forms;


namespace Samples
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			this.InitializeComponent();
            this.BindingContext = new MainViewModel();
		}


	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        this.ViewModel.Start();
	    }


	    MainViewModel ViewModel => (MainViewModel) this.BindingContext;
	}
}
