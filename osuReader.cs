using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace osu_replace
{
    class osuReaderException : Exception
    {
        public osuReaderException(string message) : base(message) { }
        public osuReaderException() : base("Something went wrong!") { }
    }

    class osuReader
    {
        private FileStream osuFile;
        private string fileName;
        private string[] osuFileLines;
        public int beatmapID { get; private set; }


        private static int OSU_VERSION = 12;

        public osuReader(string fileName)
        {
            if (File.Exists(fileName))
            {
                this.fileName = fileName;
                StreamReader fileStream;

                try
                {
                    fileStream = new StreamReader(fileName);
                    // this should be fine (memory wise) since most .osu files are small
                    osuFileLines = fileStream.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    fileStream.Close();
                }

                catch (Exception E)
                {
                    throw new osuReaderException(String.Format("Could not open file \"{0}\": {1}", fileName, E.Message));
                }     
            }

            else
                throw new osuReaderException(String.Format("File \"{0}\" does not exist! - skipping", fileName));
        }

        public void replaceImagePath(string imagePath)
        {
            Match sectionMatch, backgroundMatch;

            if (osuFileLines.Length > 1)
            {
                // check the version
                int fileVersion = 0;
                bool validFileVersion = validVersion(Regex.Match(osuFileLines[0], "v[0-9]{2}$"), ref fileVersion);

               // if (!validFileVersion)
                   // Console.WriteLine(String.Format("Warning: The .osu file \"{0}\" version is unsupported [current={1}] [file's={2}]", fileName, OSU_VERSION, fileVersion));

                if (!File.Exists(imagePath))
                    throw new osuReaderException(String.Format("The image file at \"{0}\" does not appear to exist", imagePath));

                if (!imagePath.Contains(".jpg") && !imagePath.Contains(".png"))
                    throw new osuReaderException(String.Format("The image file at \"{0}\" does not appear to be a valid image format", imagePath));

                bool inEventsSection = false;
                for (int i = 0; i < osuFileLines.Length; i++)
                {
                    sectionMatch = Regex.Match(osuFileLines[i], @"^\[.*\]$");
                    if (osuFileLines[i].Contains("BeatmapID:"))
                        Console.Write("\r                                          \rWorking on [ID={0}]", osuFileLines[i].Split(':')[1]);
                    //currentLine = osuFileLines[i];

                    if (sectionMatch.Success)
                    {
                        // we're in a section header
                        if (sectionMatch.Value == "[Events]")
                        {
                            inEventsSection = true;
                        }

                        else
                            inEventsSection = false;
                    }

                    if (inEventsSection)
                    {
                        // find the background line;
                        backgroundMatch = Regex.Match(osuFileLines[i], "0,0,\".+\"");
                        if (backgroundMatch.Success)
                        {
                            string backgroundPath = Path.GetDirectoryName(fileName) + '\\' + backgroundMatch.Value.Split('"')[1];

                            if (!File.Exists(backgroundPath + ".modified") && File.Exists(backgroundPath))
                                File.Move(backgroundPath, backgroundPath + ".modified");

                            if (!SymbolicLink.makeLink(backgroundPath, imagePath))
                                throw new osuReaderException(String.Format(Environment.NewLine + "Could not create symbolic link for file \"{0}\"", fileName));

                            return;
                        }
                    }
                }
            }

            else
                throw new osuReaderException(String.Format(Environment.NewLine + "The .osu file \"{0}\" is corrupted [length={1}]", fileName, osuFileLines.Length));
        }
            

        private bool validVersion(Match regexMatch, ref int fileVersion)
        {
            bool valid = false;

            if (regexMatch.Success)
            {
                // this will never throw an exception because it's been regex'ed
                fileVersion = Convert.ToInt32(regexMatch.Value.Substring(1));
                valid = fileVersion == OSU_VERSION;
            }

            return valid;
        }
    }
}
