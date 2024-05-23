using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Begin : ContentPage
    {
        public Begin()
        {
            InitializeComponent();
        }
        private async void OpenApp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}