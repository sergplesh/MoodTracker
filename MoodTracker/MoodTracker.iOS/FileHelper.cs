using System;
using Xamarin.Forms;
using MoodTracker.iOS;
using MoodTracker;

[assembly: Dependency(typeof(FileHelper))]
namespace MoodTracker.iOS
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = System.IO.Path.Combine(docFolder, "..", "Library", "Databases");

            if (!System.IO.Directory.Exists(libFolder))
            {
                System.IO.Directory.CreateDirectory(libFolder);
            }

            return System.IO.Path.Combine(libFolder, filename);
        }

        public string GetImagePath(string imageName)
        {
            // Путь для использования в Image.Source
            return imageName;
        }
    }
}
