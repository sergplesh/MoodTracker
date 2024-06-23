using SQLite;
using System.Collections.Generic; 
using System.Threading.Tasks;
using MoodTracker.Models; // Подключение пространства имен, содержащего модель MoodEntry.

namespace MoodTracker.Data
{
    public class MoodDatabase // класс для взаимодействия с базой данных SQLite
    {
        // Атрибут для соединения с базой данных SQLite.
        private readonly SQLiteConnection database;

        // Конструктор класса MoodDatabase (параметр - путь к базе данных)
        public MoodDatabase(string dbPath)
        {
            // Инициализация соединения с базой данных по указанному пути.
            database = new SQLiteConnection(dbPath);
            // Создание таблицы с записями настроения
            database.CreateTable<MoodEntry>();
        }

        // Метод для получения всех записей настроений из базы данных
        public List<MoodEntry> GetMoods()
        {
            // Возвращает список всех записей из таблицы с записями настроения
            return database.Table<MoodEntry>().ToList();
        }

        // Метод для сохранения (вставки или обновления) записи настроения в базу данных
        public int SaveMood(MoodEntry mood)
        {
            // Если запись уже существует (Id не равен 0), обновляем ее
            if (mood.Id != 0)
            {
                return database.Update(mood);
            }
            // Если запись новая (Id равен 0), вставляем ее
            else
            {
                return database.Insert(mood);
            }
        }

        // Метод для удаления записи настроения из базы данных
        public int DeleteMood(MoodEntry mood)
        {
            return database.Delete(mood); // Удаление записи из таблицы с записями настроения
        }
    }
}
