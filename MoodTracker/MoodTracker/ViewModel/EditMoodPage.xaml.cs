using MoodTracker.Models;
using MoodTracker.ViewModel;
using System;
using Xamarin.Forms;

namespace MoodTracker
{
    public partial class EditMoodPage : ContentPage
    {
        private MoodEntry moodEntry;

        public EditMoodPage(MoodEntry moodEntry)
        {
            InitializeComponent();
            this.moodEntry = moodEntry;
            LoadMood();
        }

        private void LoadMood()
        {
            DatePicker.Date = moodEntry.Date;
            MoodPicker.SelectedItem = moodEntry.Mood;
            NotesEditor.Text = moodEntry.Notes;
        }

        private async void OnSaveChangesClicked(object sender, EventArgs e)
        {
            // Обновление настроения
            moodEntry.Date = DatePicker.Date;
            moodEntry.Mood = MoodPicker.SelectedItem?.ToString();
            moodEntry.Notes = NotesEditor.Text;
            moodEntry.MoodImage = MainPage.GetMoodImage(moodEntry.Mood);

            MainPage.Database.SaveMood(moodEntry);

            OnMoodUpdated(this, new MoodEventArgs(moodEntry));
            await Navigation.PopAsync();
        }

        public void OnMoodUpdated(object sender, MoodEventArgs e)
        {
            moodEntry = e.MoodEntry;
            MoodUpdated?.Invoke(sender, moodEntry);
        }

        public event EventHandler<MoodEntry> MoodUpdated;
    }
}
