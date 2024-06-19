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
    public partial class Begin : ContentPage // начальная страница входа в приложение
    {
        /// <summary>
        /// Вывод объектов (кнопки, текст и тд) на экран начальной страницы
        /// </summary>
        public Begin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод для навигации с начального окна к странице для создания записи настроения
        /// </summary>
        private async void OpenApp(object sender, EventArgs e)
        {
            // Переход на страницу создания записей (добавление в навигационный стек страницы для создания записи настроения)
            await Navigation.PushAsync(new MainPage()); 
        }
    }
}