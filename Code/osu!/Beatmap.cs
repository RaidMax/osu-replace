using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace OsuReplace.Code.osu
{
    public class BeatmapState
    {
        public string CurrentBeatmap { get; set; }
    }

    public class Beatmaps
    {
        private string[] BeatmapFolders;
        private string Path;
        private int CurrentIndex;
        private string ImagePath;

        public Beatmaps(string path, string imgPath)
        {
            Path = path;
            ImagePath = imgPath;
            CurrentIndex = 0;
            Load();
        }

        private void Load()
        {
            BeatmapFolders = Directory.GetDirectories(Path);
        }

        public bool ReplaceNext(bool restore = false)
        {
            var directoryFiles = Directory.EnumerateFiles($"{BeatmapFolders[CurrentIndex]}");
            string[] beatmapFiles = directoryFiles.Where(f => f.Contains(".osu")).ToArray();
            string regexString = "0,0,\".+\\..+\".*";
            bool success = true;

            if (beatmapFiles.FirstOrDefault(f => f.Contains(".replaced")) != null)
                return false;

            foreach (string beatmap in beatmapFiles)
            {
                string[] beatmapFile = File.ReadAllLines(beatmap);

                foreach (string line in beatmapFile)
                {
                    var match = Regex.Match(line, regexString);
                    if (match.Success)
                    {
                        string imagePath = Regex.Match(match.Value, "\".+\\..+\"").Value;
                        imagePath = $"{BeatmapFolders[CurrentIndex]}{System.IO.Path.DirectorySeparatorChar}{imagePath.Substring(1, imagePath.Length - 2)}";
                        try
                        {
                            bool symLink = (File.GetAttributes(imagePath) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint;
                            if (!restore)
                            {
                                if (!symLink)
                                {
                                    File.Move(imagePath, $"{imagePath}.replaced");
                                    SymbolicLink.MakeSymbolicLink(imagePath, ImagePath);
                                }

                                else
                                    success = false;
                            }

                            else 
                            {
                                if (symLink)
                                {
                                    File.Delete(imagePath);
                                    File.Move($"{imagePath}.replaced", imagePath);
                                }

                                else
                                    success = false;
                            }
                        }

                        catch (FileNotFoundException)
                        {
                            success = false;
                        }
                        // stop after the first image occurance 
                        break;
                    }
                }
            }

            CurrentIndex++;
            return success;
        }

        public int GetReplacementProgress()
        {
            float prog = ((float)CurrentIndex / (float)BeatmapFolders.Count()) * 100;
            return (int)prog;
        }

        public string GetCurrentBeatmap()
        {
            return BeatmapFolders[CurrentIndex];
        }

        public bool ReplacementFinished()
        {
            return CurrentIndex == BeatmapFolders.Count();
        }

        public int GetNumberBeatmaps()
        {
            return BeatmapFolders.Count();
        }
    }
}
