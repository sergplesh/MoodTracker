using MoodTracker.Models;
using System;
using Xamarin.Forms;

namespace MoodTracker
{
    public partial class EditMoodPage : ContentPage
    {
        private MoodEntry _moodEntry;

        public EditMoodPage(MoodEntry moodEntry)
        {
            InitializeComponent();
            _moodEntry = moodEntry;
            LoadMood();
        }

        private void LoadMood()
        {
            DatePicker.Date = _moodEntry.Date;
            MoodPicker.SelectedItem = _moodEntry.Mood;
            NotesEditor.Text = _moodEntry.Notes;
        }

        private async void OnSaveChangesClicked(object sender, EventArgs e)
        {
            // Обновление настроения
            _moodEntry.Date = DatePicker.Date;
            _moodEntry.Mood = MoodPicker.SelectedItem?.ToString();
            _moodEntry.Notes = NotesEditor.Text;

            var fileHelper = DependencyService.Get<IFileHelper>();
            switch (_moodEntry.Mood)
            {
                case "Отличное":
                    _moodEntry.MoodImage = fileHelper.GetImagePath("excellent.png");
                    break;
                case "Хорошее":
                    _moodEntry.MoodImage = fileHelper.GetImagePath("good.png");
                    break;
                case "Нормальное":
                    _moodEntry.MoodImage = fileHelper.GetImagePath("normal.png");
                    break;
                case "Плохое":
                    _moodEntry.MoodImage = fileHelper.GetImagePath("bad.png");
                    break;
                case "Ужасное":
                    _moodEntry.MoodImage = fileHelper.GetImagePath("terrible.png");
                    break;
                default:
                    _moodEntry.MoodImage = null;
                    break;
            }

            await MainPage.Database.SaveMoodAsync(_moodEntry);
            MoodUpdated?.Invoke(this, _moodEntry);
            await Navigation.PopAsync();
        }

        public event EventHandler<MoodEntry> MoodUpdated;
    }
}
