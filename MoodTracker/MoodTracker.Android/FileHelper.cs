using System;
using System.IO;
using Xamarin.Forms;
using MoodTracker.Droid;
using MoodTracker;

[assembly: Dependency(typeof(FileHelper))]
namespace MoodTracker.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public string GetImagePath(string imageName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), imageName);
        }
    }
}
