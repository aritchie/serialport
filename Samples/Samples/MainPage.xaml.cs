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
	}
}
