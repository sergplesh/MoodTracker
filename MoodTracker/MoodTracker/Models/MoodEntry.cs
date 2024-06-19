using SQLite;
using System;

namespace MoodTracker.Models
{
    /// <summary>
    /// Запись настроения
    /// </summary>
    public class MoodEntry
    {
        [PrimaryKey, AutoIncrement] // будет автоматически определяться при добавлении в БД
        public int Id { get; set; } // id в базе данных
        public DateTime Date { get; set; } // дата записи
        public string Mood { get; set; } // настроение
        public string Notes { get; set; } // запись впечатлений за прошедший день
        public string MoodImage { get; set; } // свойство для хранения пути к картинке настроения
    }
}
