using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OsuReplace.Code.Utilities;

public class Configuration(string fileName)
{
    private readonly Dictionary<string, string> _settings = new();

    public Configuration Load()
    {
        var configLines = File.ReadAllLines(fileName);
        foreach (var line in configLines)
        {
            if (line.Length is 0 || line.First() is '#') continue;

            var split = line.Split('=');

            if (split.Length > 1)
            {
                _settings.Add(split[0].Trim(), split[1].Trim());
            }
        }

        return this;
    }

    public string GetValue(string key) => _settings.GetValueOrDefault(key, string.Empty);
}
