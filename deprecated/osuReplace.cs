using System;
using System.IO;
using System.Collections.Generic;

namespace OsuReplace
{
    class osuReplace
    {
        public static List<bool> replaceImages(string beatmapBasePath, string replacementImagePath)
        {
            List<bool> resultSet = new List<bool>();
            try
            {
                string[] folderNames = Directory.GetDirectories(beatmapBasePath);
                decimal folderSize = folderNames.Length;
                decimal currentBeatmap = 1;

                foreach (string beatmapFolder in folderNames)
                {
                    string osuFile = String.Empty;
                    bool alreadyModified = false;
                    foreach (string beatmapFile in Directory.GetFiles(beatmapFolder))
                    {

                        if (beatmapFile.Contains(".osu"))
                            osuFile = beatmapFile;
                        if (beatmapFile.Contains(".modified"))
                            alreadyModified = true;
                    }

                    if (!alreadyModified && osuFile != String.Empty)
                    {
                        try
                        {
                            osuReader _file = new osuReader(osuFile);
                            _file.replaceImagePath(replacementImagePath);
                            resultSet.Add(true);
                            // this is messy here, but no place else to put it at the moment
                            string progressText = "------------------------------------";
                            string progressRemainingText = "                                    ";
                            decimal progress = Math.Ceiling((currentBeatmap / folderSize) * progressText.Length);
                            decimal progressRemaining = Math.Floor(((folderSize - currentBeatmap) / folderSize) * progressRemainingText.Length);
                            Console.Write("\r[{0}] {1}%", progressText.Substring(0, Convert.ToInt32(progress)) + progressRemainingText.Substring(0, Convert.ToInt32(progressRemaining)), ((progress / progressText.Length) * 100).ToString("##.#"));
                            if (currentBeatmap == folderSize - 1)
                                Console.Write(Environment.NewLine);
                        }

                        catch (osuReaderException Error)
                        {
                            //Console.WriteLine("Error: " + Error.Message);
                            resultSet.Add(false);
                        }
                    }

                    else
                        resultSet.Add(false);
                    currentBeatmap++;
                }
            }

            catch (Exception E)
            {
                Console.WriteLine("Error: " + E.Message);
            }

            return resultSet;
        }

        public static List<bool> revertImages(string beatmapBasePath)
        {
            List<bool> resultSet = new List<bool>();
            try
            {
                string[] folderNames = Directory.GetDirectories(beatmapBasePath);
                decimal folderSize = folderNames.Length;
                decimal currentBeatmap = 1;

                foreach (string beatmapFolder in folderNames)
                {
                    foreach (string beatmapFile in Directory.GetFiles(beatmapFolder))
                    {
                        if (beatmapFile.Contains(".modified"))
                        {
                            try
                            {
                                string originalFileName = beatmapFile.Substring(0, beatmapFile.Length - 9);
                                File.Delete(originalFileName);
                                File.Move(beatmapFile, originalFileName);
                                resultSet.Add(true);

                                // this is messy here, but no place else to put it at the moment
                                string progressText = "------------------------------------";
                                string progressRemainingText = "                                    ";
                                decimal progress = Math.Ceiling((currentBeatmap / folderSize) * progressText.Length);
                                decimal progressRemaining = Math.Floor(((folderSize - currentBeatmap) / folderSize) * progressRemainingText.Length);
                                Console.Write("\r[{0}] {1}%", progressText.Substring(0, Convert.ToInt32(progress)) + progressRemainingText.Substring(0, Convert.ToInt32(progressRemaining)), ((progress / progressText.Length) * 100).ToString("##.#"));
                                if (currentBeatmap == folderSize - 1)
                                    Console.Write(Environment.NewLine);
                            }

                            catch(Exception)
                            {
                                resultSet.Add(false);
                            }
                        }                
                    }
                    currentBeatmap++;
                }
            }

            catch (Exception E)
            {
                Console.WriteLine("Error: " + E.Message);
            }

            return resultSet;
        }
    }
}
