using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoodTracker.Models;

namespace MoodTracker.Data
{
    public class MoodDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public MoodDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MoodEntry>().Wait();
        }

        public Task<List<MoodEntry>> GetMoodsAsync()
        {
            return _database.Table<MoodEntry>().ToListAsync();
        }

        public Task<int> SaveMoodAsync(MoodEntry mood)
        {
            if (mood.Id != 0)
            {
                return _database.UpdateAsync(mood);
            }
            else
            {
                return _database.InsertAsync(mood);
            }
        }

        public Task<int> DeleteMoodAsync(MoodEntry mood)
        {
            return _database.DeleteAsync(mood);
        }
    }
}
