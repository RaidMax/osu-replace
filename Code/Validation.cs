using System.IO;
using System.Linq;

namespace OsuReplace.Code
{
    public class Validation
    {
        public static bool ValidOsuDirectory(string directory)
        {
            string[] directoryFiles = Directory.GetFiles(directory);
            return directoryFiles.SingleOrDefault(f => f == $"{directory}{Path.DirectorySeparatorChar}osu!.exe") != null;
        }
    }
}
