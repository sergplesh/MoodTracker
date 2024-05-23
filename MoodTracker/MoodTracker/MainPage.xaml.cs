using MoodTracker.Data;
using MoodTracker.Models;
using System;
using Xamarin.Forms;

namespace MoodTracker
{
    public partial class MainPage : ContentPage
    {
        private static MoodDatabase _database;

        public static MoodDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new MoodDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("MoodSQLite.db3"));
                }
                return _database;
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSaveMoodClicked(object sender, EventArgs e)
        {
            if (MoodPicker.SelectedItem == null)
            {
                await DisplayAlert("Ошибка", "Пожалуйста, выберите настроение.", "OK");
                return;
            }

            var selectedMood = MoodPicker.SelectedItem.ToString();
            var moodImage = GetMoodImage(selectedMood);

            var mood = new MoodEntry
            {
                Date = DateTime.Now,
                Mood = selectedMood,
                Notes = NotesEditor.Text,
                MoodImage = moodImage
            };

            await Database.SaveMoodAsync(mood);
            MoodPicker.SelectedIndex = -1;
            NotesEditor.Text = string.Empty;
        }

        private async void OnViewSavedMoodsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SavedMoodsPage());
        }

        private string GetMoodImage(string mood)
        {
            var fileHelper = DependencyService.Get<IFileHelper>();
            switch (mood)
            {
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
    }
}
