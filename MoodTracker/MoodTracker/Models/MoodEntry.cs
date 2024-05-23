using SQLite;
using System;

namespace MoodTracker.Models
{
    public class MoodEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Mood { get; set; }
        public string Notes { get; set; }
        public string MoodImage { get; set; } // Новое свойство для хранения пути к картинке настроения
    }
}
