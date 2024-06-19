using MoodTracker.Data;
using MoodTracker.Models;
using System;
using System.Linq;
using Xamarin.Forms;

namespace MoodTracker
{
    /// <summary>
    /// страница для создания записи настроения
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private static MoodDatabase database; // связь с БД

        public static MoodDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MoodDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("MoodSQLite.db3"));
                }
                return database;
            }
        }

        /// <summary>
        /// Конструктор выводящий объекты (кнопки, текст и тд) на экран начальной страницы
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSaveMoodClicked(object sender, EventArgs e)
        {
            // если настроение не выбрано, то сообщаем о необходимости его выбрать
            if (MoodPicker.SelectedItem == null)
            {
                await DisplayAlert("Ошибка!", "Пожалуйста, выберите настроение.", "Ок");
                return;
            }

            // выбранное настроение
            var selectedMood = MoodPicker.SelectedItem.ToString(); 
            // получаем путь к картинке, соответствующей настроению
            var moodImage = GetMoodImage(selectedMood);

            // создаём запись
            var mood = new MoodEntry
            {
                Date = DateTime.Now,
                Mood = selectedMood,
                Notes = NotesEditor.Text,
                MoodImage = moodImage
            };

            // сохраняем в таблицу БД сделанную запись
            await Database.SaveMoodAsync(mood);

            //// Вставляем новую запись в начало списка на странице сохраненных настроений
            //if (Navigation.NavigationStack.LastOrDefault() is SavedMoodsPage savedMoodsPage)
            //{
            //    savedMoodsPage.InsertMoodAtStart(mood);
            //}

            MoodPicker.SelectedIndex = -1;
            NotesEditor.Text = string.Empty;
        }

        /// <summary>
        /// Переход в дневник с записями
        /// </summary>
        private async void OnViewSavedMoodsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SavedMoodsPage());
        }

        /// <summary>
        /// Получение пути к картинке, показывающей настроение
        /// </summary>
        private string GetMoodImage(string mood)
        {
            var fileHelper = DependencyService.Get<IFileHelper>();
            switch (mood)
            {
                // в зависимости от выбора пользователя устанавливаем картинку-настроение
                case "Отличное":
                    return fileHelper.GetImagePath("excellent.png");
                case "Хорошее":
                    return fileHelper.GetImagePath("good.png");
                case "Нормальное":
                    return fileHelper.GetImagePath("normal.png");
                case "Плохое":
                    return fileHelper.GetImagePath("bad.png");
                case "Ужасное":
                    return fileHelper.GetImagePath("terrible.png");
                default:
                    return null;
            }
        }

        /// <summary>
        /// Метод для перехода в дневник с записями кликом по картинке
        /// </summary>
        private async void OnImageTapped(object sender, EventArgs e)
        {
            // Переход на страницу сохраненных настроений (добавление в навигационный стек страницы для создания записи настроения)
            await Navigation.PushAsync(new SavedMoodsPage());
        }
    }
}
