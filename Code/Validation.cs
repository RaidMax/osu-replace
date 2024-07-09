using System.IO;
using System.Linq;

namespace OsuReplace.Code;

public abstract class Validation
{
    public static bool ValidOsuDirectory(string directory)
    {
        var directoryFiles = Directory.GetFiles(directory);
        return directoryFiles.SingleOrDefault(f => f == $"{directory}{Path.DirectorySeparatorChar}osu!.exe") != null;
    }
}
