using System;
using System.Collections.Generic;
using System.IO;

namespace OsuReplace.Code.osu
{
    public class Configuration
    {
        private Dictionary<string, string> _settings;
        private string FileName;

        public Configuration(string fileName)
        {
            _settings = new Dictionary<string, string>();
            FileName = fileName;
            Load();
        }

        private void Load()
        {
            string[] configLines = File.ReadAllLines(FileName);
            foreach(string line in configLines)
            {
                 if (line.Length == 0 || line[0] == '#')
                    continue;
                string[] split = line.Split('=');
                if (split.Length > 1)
                    _settings.Add(split[0].Trim(), split[1].Trim());
            }
        }

        public string GetValue(string key)
        {
            if (_settings.ContainsKey(key))
                return _settings[key];
            return "";
        }
    }
}
