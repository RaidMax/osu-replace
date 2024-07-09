using System.Collections.Generic;
using System.IO;
using Serilog;
using System.Linq;
using System.Text.RegularExpressions;
using Serilog.Core;

namespace OsuReplace.Code.Utilities;

public partial class BeatMap(string path, IEnumerable<string> paths)
{
    private readonly string[] _paths = paths.ToArray();
    private int _currentIndex;

    public int BackgroundsChanged { get; private set; }
    public int BeatMapsBackgroundNotFound { get; private set; }
    public int SkippedBeatMaps { get; private set; }

    public void ReplaceNext(bool restore = false)
    {
        var beatMapFiles = Directory.EnumerateFiles(_paths[_currentIndex], "*.osu").ToArray();

        Start.Logger.Debug("Iterating: {FileCount}", beatMapFiles.Length);

        foreach (var beatMap in beatMapFiles)
        {
            ProcessBeatMap(beatMap, restore);
        }

        _currentIndex++;
        Start.Logger.Information("Progress: {ReplacementProgress}%", GetReplacementProgress());
    }

    private void ProcessBeatMap(string beatMapPath, bool restore)
    {
        var hasBackground = false;
        var beatMapLines = File.ReadAllLines(beatMapPath);
        Start.Logger.Debug("File: {BeatMap}, Lines: {Lines}", beatMapPath, beatMapLines.Length);

        foreach (var line in beatMapLines)
        {
            var match = BeatMapRegex().Match(line);
            if (!match.Success)
            {
                continue;
            }

            var imagePath = ImageRegex().Match(match.Value).Value;
            imagePath = Path.Combine(_paths[_currentIndex], imagePath.Replace("\"", ""));

            ProcessImage(imagePath, restore);
            hasBackground = true;

            break; // Only process the first image occurrence
        }

        if (!hasBackground)
        {
            BeatMapsBackgroundNotFound++;
        }
    }

    private void ProcessImage(string imagePath, bool restore)
    {
        try
        {
            if (!File.Exists(imagePath))
            {
                HandleMissingImage(imagePath, restore);
                return;
            }

            var isSymLink = (File.GetAttributes(imagePath) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint;

            if (!restore)
            {
                ReplaceImage(imagePath, isSymLink);
            }
            else
            {
                RestoreImage(imagePath, isSymLink);
            }
        }
        catch (FileNotFoundException ex)
        {
            Start.Logger.Error("Failed to process image: {ImagePath}: {ExMessage}", imagePath, ex.Message);
            File.CreateSymbolicLink(imagePath, path);
        }
    }

    private void HandleMissingImage(string imagePath, bool restore)
    {
        if (restore && File.Exists($"{imagePath}.replaced"))
        {
            BackgroundsChanged++;
            File.Move($"{imagePath}.replaced", imagePath);
            Start.Logger.Information("Restored {ImagePath}", imagePath);
        }
        else
        {
            Start.Logger.Warning("{ImagePath} missing", imagePath);
        }
    }

    private void ReplaceImage(string imagePath, bool isSymLink)
    {
        if (isSymLink)
        {
            SkippedBeatMaps++;
            return;
        }

        File.Move(imagePath, $"{imagePath}.replaced");
        var symbolicLinkInfo = File.CreateSymbolicLink(imagePath, path);
        if (symbolicLinkInfo.LinkTarget != path)
        {
            // revert the change because the symlink failed
            File.Move($"{imagePath}.replaced", imagePath);
            Start.Logger.Error("Failed to create symbolic link for {ImagePath}", imagePath);
        }
        else
        {
            Start.Logger.Information("Replaced {ImagePath}", imagePath);
            BackgroundsChanged++;
        }
    }

    private void RestoreImage(string imagePath, bool isSymLink)
    {
        if (!isSymLink)
        {
            SkippedBeatMaps++;
            return;
        }

        File.Delete(imagePath);

        if (!File.Exists($"{imagePath}.replaced"))
        {
            File.Copy(path, imagePath);
            Start.Logger.Warning("{ImagePath} missing, copied default image", imagePath);
        }
        else
        {
            File.Move($"{imagePath}.replaced", imagePath);
            BackgroundsChanged++;
        }

        Start.Logger.Information("Restored {ImagePath}", imagePath);
    }

    public int GetReplacementProgress() => (int)(_currentIndex / (float)_paths.Length * 100);
    public string GetCurrentBeatMap() => _paths[_currentIndex];
    public bool ReplacementFinished() => _currentIndex == _paths.Length;

    [GeneratedRegex("\".+\\..+\"")]
    private static partial Regex ImageRegex();

    [GeneratedRegex("0,0,\".+\\..+\".*")]
    private static partial Regex BeatMapRegex();
}
