using System;
using System.IO;
using System.Collections.Generic;

namespace osu_replace
{
    class osuReplace
    {
        public static List<bool> replaceImages(string beatmapBasePath, string replacementImagePath)
        {
            List<bool> resultSet = new List<bool>();
            try
            {
                foreach (string beatmapFolder in Directory.GetDirectories(beatmapBasePath))
                {
                    foreach (string beatmapFile in Directory.GetFiles(beatmapFolder))
                    {
                        if (beatmapFile.Contains(".osu"))
                        {
                            try
                            {
                                osuReader _file = new osuReader(beatmapFile);
                                _file.replaceImagePath(replacementImagePath);
                                resultSet.Add(true);
                            }

                            catch (osuReaderException Error)
                            {
                                Console.WriteLine("Error: " + Error.Message);
                                resultSet.Add(false);
                            }

                            // only need to go through one .osu file to find the background
                            break;
                        }
                    }
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
                foreach (string beatmapFolder in Directory.GetDirectories(beatmapBasePath))
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
                            }

                            catch(Exception)
                            {
                                resultSet.Add(false);
                            }
                        }                
                    }
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
