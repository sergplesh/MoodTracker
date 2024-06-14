using MoodTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MoodTracker
{
    public partial class SavedMoodsPage : ContentPage
    {
        private DateTime _lastTapTime;
        private const int DoubleTapDelay = 300; // Время в миллисекундах, в течение которого должно произойти двойное нажатие

        private List<MoodEntry> _allMoods; // Полный список настроений

        public SavedMoodsPage()
        {
            InitializeComponent();
            LoadMoods();
        }

        private async void LoadMoods()
        {
            _allMoods = await MainPage.Database.GetMoodsAsync();
            MoodListView.ItemsSource = _allMoods.OrderByDescending(m => m.Date).ToList();
        }

        private void OnMonthYearSelected(object sender, EventArgs e)
        {
            FilterMoods();
        }

        private void OnYearEntryChanged(object sender, TextChangedEventArgs e)
        {
            FilterMoods();
        }

        // Обработчик нажатия на кнопку "Scroll to Top"
        private void OnScrollToTopClicked(object sender, EventArgs e)
        {
            if (MoodListView.ItemsSource != null && _allMoods.Any())
            {
                MoodListView.ScrollTo(_allMoods.First(), ScrollToPosition.Start, true);
            }
        }

        private void FilterMoods()
        {
            IEnumerable<MoodEntry> filteredMoods = _allMoods;

            if (MonthPicker.SelectedIndex > 0) // Индекс 0 для "Все месяцы"
            {
                int selectedMonth = MonthPicker.SelectedIndex; // Месяцы в DateTime начинаются с 1 (январь)
                filteredMoods = filteredMoods.Where(m => m.Date.Month == selectedMonth);
            }

            if (int.TryParse(YearEntry.Text, out int selectedYear))
            {
                filteredMoods = filteredMoods.Where(m => m.Date.Year == selectedYear);
            }

            MoodListView.ItemsSource = filteredMoods.OrderByDescending(m => m.Date).ToList();
        }

        private async void OnMoodItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is MoodEntry mood)
            {
                var currentTapTime = DateTime.Now;
                var timeDifference = currentTapTime - _lastTapTime;

                if (timeDifference.TotalMilliseconds < DoubleTapDelay)
                {
                    await DisplayAlert("Notes", mood.Notes, "OK");
                }

                _lastTapTime = currentTapTime;
            }

            ((ListView)sender).SelectedItem = null;
        }

        // метод для удаления записи
        private async void OnDeleteMoodClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var mood = button.BindingContext as MoodEntry;

            if (mood != null)
            {
                bool isConfirmed = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this mood?", "Yes", "No");
                if (isConfirmed)
                {
                    await MainPage.Database.DeleteMoodAsync(mood);
                    _allMoods.Remove(mood);
                    FilterMoods();
                }
            }
        }

        // Метод для вставки новой записи в начало списка
        public void InsertMoodAtStart(MoodEntry mood)
        {
            _allMoods.Insert(0, mood);
            MoodListView.ItemsSource = _allMoods.OrderByDescending(m => m.Date).ToList();
        }
    }
}