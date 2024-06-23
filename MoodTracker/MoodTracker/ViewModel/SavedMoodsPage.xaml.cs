using MoodTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MoodTracker
{
    public partial class SavedMoodsPage : ContentPage
    {
        private DateTime lastTapTime;

        // Время в миллисекундах, в течение которого должно произойти двойное нажатие
        private const int DoubleTapDelay = 300;

        /// <summary>
        /// Полный список настроений
        /// </summary>
        private List<MoodEntry> allMoods; 

        public SavedMoodsPage()
        {
            InitializeComponent();
            LoadMoods();
        }

        private void LoadMoods()
        {
            allMoods = MainPage.Database.GetMoods();
            allMoods = allMoods.OrderByDescending(m => m.Date).ToList();
            MoodListView.ItemsSource = allMoods;
        }

        /// <summary>
        /// Обработчик события выбора месяца и года
        /// </summary>
        private void OnMonthYearSelected(object sender, EventArgs e)
        {
            FilterMoods(); // Фильтрация списка настроений
        }

        /// <summary>
        /// Обработчик события изменения года
        /// </summary>
        private void OnYearEntryChanged(object sender, TextChangedEventArgs e)
        {
            FilterMoods(); // Фильтрация списка настроений
        }

        /// <summary>
        /// Возврат в начало списка
        /// </summary>
        private void OnScrollToTopClicked(object sender, EventArgs e)
        {
            if (MoodListView.ItemsSource != null && allMoods.Any())
            {
                MoodListView.ScrollTo(allMoods.First(), ScrollToPosition.Start, true);
            }
        }

        /// <summary>
        /// Метод для фильтрации настроений по выбранному месяцу и году
        /// </summary>
        private void FilterMoods()
        {
            IEnumerable<MoodEntry> filteredMoods = allMoods;

            if (MonthPicker.SelectedIndex > 0) // Индекс 0 для "Все месяцы"
            {
                int selectedMonth = MonthPicker.SelectedIndex; // Месяцы в DateTime начинаются с 1 (январь)
                filteredMoods = filteredMoods.Where(m => m.Date.Month == selectedMonth); // Фильтрация по месяцу
            }

            if (int.TryParse(YearEntry.Text, out int selectedYear))
            {
                filteredMoods = filteredMoods.Where(m => m.Date.Year == selectedYear); // Фильтрация по году
            }

            // Обновление источника данных для ListView, отсортированного по дате в порядке убывания
            MoodListView.ItemsSource = filteredMoods.OrderByDescending(m => m.Date).ToList();
        }

        /// <summary>
        /// Обработчик нажатия на элемент настроения в списке записей
        /// </summary>
        private async void OnMoodItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is MoodEntry mood)
            {
                var currentTapTime = DateTime.Now; // Текущее время нажатия
                var timeDifference = currentTapTime - lastTapTime; // Разница во времени между нажатиями

                if (timeDifference.TotalMilliseconds < DoubleTapDelay)
                {
                    // Показать заметки, если произошло двойное нажатие
                    await DisplayAlert("Ваша запись:", mood.Notes, "Закрыть");
                }

                lastTapTime = currentTapTime; // Обновление времени последнего нажатия
            }

            ((ListView)sender).SelectedItem = null; // Снятие выделения с элемента
        }

        /// <summary>
        /// Метод для удаления записи
        /// </summary>
        private async void OnDeleteMoodClicked(object sender, EventArgs e)
        {
            var button = sender as Button; // Кнопка удаления
            MoodEntry mood = button.BindingContext as MoodEntry; // Получение записи настроения

            if (mood != null)
            {
                bool isConfirmed = await DisplayAlert("Подтверждение удаления", "Вы действительно хотите удалить запись?", "Да", "Нет");
                if (isConfirmed)
                {
                    // Удаление записи из базы данных и списка
                    MainPage.Database.DeleteMood(mood);
                    allMoods.Remove(mood);
                    FilterMoods(); // Обновление списка настроений
                }
            }
        }

        /// <summary>
        /// Метод для редактирования записи
        /// </summary>
        private async void OnEditMoodClicked(object sender, EventArgs e)
        {
            var button = sender as Button; // Кнопка редактирования
            var mood = button.BindingContext as MoodEntry; // Получение записи настроения

            if (mood != null)
            {
                // Создание страницы редактирования и обработка обновления настроения
                var editPage = new EditMoodPage(mood); // в параметр передаём запись настроения MoodEntry
                editPage.MoodUpdated += (source, updatedMood) =>
                {
                    var index = allMoods.IndexOf(mood); // Поиск индекса редактируемого настроения
                    MainPage.Database.DeleteMood(allMoods[index]); // удаление из таблицы бд прошлой записи
                    allMoods[index] = updatedMood; // Обновление настроения в списке
                    FilterMoods(); // Обновление списка настроений
                };
                await Navigation.PushAsync(editPage); // Переход на страницу редактирования
            }
        }
    }
}