﻿using Xamarin.Forms;
using System.Threading.Tasks;
using Acquaint.Util;

namespace Acquaint.XForms
{
	/// <summary>
	/// Splash Page that is used on Androd only. iOS splash characteristics are NOT defined here, ub tn the iOS prject settings.
	/// </summary>
	public partial class SplashPage : ContentPage
	{
		bool _ShouldDelayForSplash = true;

		public SplashPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (_ShouldDelayForSplash)
				// delay for a few seconds on the splash screen
				await Task.Delay(3000);

			// if a data partition phrase has not yet been set
			if (string.IsNullOrWhiteSpace(Settings.DataPartitionPhrase))
			{
				// modally push a new SetupPage wrapped in a NavigationPage
				await Navigation.PushModalAsync(new NavigationPage(new SetupPage()));
				_ShouldDelayForSplash = false;
			}
			else
			{
				// create a new NavigationPage, with a new AcquaintanceListPage set as the Root
				var navPage = new NavigationPage(
					new AcquaintanceListPage()
					{
						Title = "Acquaintances",
						BindingContext = new AcquaintanceListViewModel()
					})
				{
					BarTextColor = Color.White // Ensures statusbar text color on iOS is white. Also set "View controller-based status bar appearance" to "No" in Info.plist on iOS.
				};

				// set the MainPage of the app to the navPage
				Application.Current.MainPage = navPage;
			}
		}
	}
}

