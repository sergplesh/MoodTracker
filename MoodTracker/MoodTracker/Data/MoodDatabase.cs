using SQLite;
using System.Collections.Generic; 
using System.Threading.Tasks;
using MoodTracker.Models; // Подключение пространства имен, содержащего модель MoodEntry.

namespace MoodTracker.Data
{
    public class MoodDatabase // класс для взаимодействия с базой данных SQLite
    {
        // Атрибут для асинхронного соединения с базой данных SQLite.
        private readonly SQLiteAsyncConnection database;

        // Конструктор класса MoodDatabase (параметр - путь к базе данных)
        public MoodDatabase(string dbPath)
        {
            // Инициализация соединения с базой данных по указанному пути.
            database = new SQLiteAsyncConnection(dbPath);
            // Создание таблицы с записями настроения
            database.CreateTableAsync<MoodEntry>();
        }

        // Метод для получения всех записей настроений из базы данных
        public Task<List<MoodEntry>> GetMoodsAsync()
        {
            // Возвращает список всех записей из таблицы с записями настроения в виде асинхронной задачи
            return database.Table<MoodEntry>().ToListAsync();
        }

        // Метод для сохранения (вставки или обновления) записи настроения в базу данных
        public Task<int> SaveMoodAsync(MoodEntry mood)
        {
            // Если запись уже существует (Id не равен 0), обновляем ее
            if (mood.Id != 0)
            {
                return database.UpdateAsync(mood);
            }
            // Если запись новая (Id равен 0), вставляем ее
            else
            {
                return database.InsertAsync(mood);
            }
        }

        // Метод для удаления записи настроения из базы данных
        public Task<int> DeleteMoodAsync(MoodEntry mood)
        {
            return database.DeleteAsync(mood); // Удаление записи из таблицы с записями настроения
        }
    }
}
