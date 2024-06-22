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
            OnRandomSupportiveMessage();
        }

        private List<string> messages = new List<string>
        {
            "Каждый день - это новая возможность",
            "Оставайся на позитиве и двигайся вперёд",
            "У тебя есть силы стремиться к лучшему",
            "Верь в себя и ты свернёшь горы",
            "Ничего не бойся - только так сбываются мечты"
        };

        private Random random = new Random();

        /// <summary>
        /// Метод для навигации с начального окна к странице для создания записи настроения
        /// </summary>
        private async void OnOpenApp(object sender, EventArgs e)
        {
            // Переход на страницу создания записей (добавление в навигационный стек страницы для создания записи настроения)
            await Navigation.PushAsync(new MainPage()); 
        }
        private void OnRandomSupportiveMessage()
        {
            int index = random.Next(messages.Count);
            SupportiveMessageLabel.Text = messages[index];
        }
    }
}