using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XappPomodoro.Views;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace XappPomodoro
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
			MainPage = new RootPage();  //MainPage();            
            //MainPage = new PomodoroPage();      
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
