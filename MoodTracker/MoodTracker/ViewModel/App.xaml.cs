using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Begin()); // в качестве главной (начальной) страницы устанавливаем входное окно Begin
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
