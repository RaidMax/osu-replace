using System;
using System.IO;

namespace osu_replace
{
    class Run
    {
        static void Main(string[] args)
        {
            if (!cmdParsing.Load(args))
            {
                Console.WriteLine("Invalid arguments.");
                Console.ReadLine();
            }

            string beatmapDirectory = "Songs";

            if (cmdParsing.beatmapDirectory != null)
                beatmapDirectory = cmdParsing.beatmapDirectory;

            if (!Directory.Exists(beatmapDirectory))
            {
                Console.WriteLine("Please put me in your osu! game directory.");
                Console.ReadKey();
                return;
            }

            if (!cmdParsing.restoreImages)
            {
                Console.WriteLine("Replacing beatmap images...");

                if (cmdParsing.backgroundColor != null)
                    osuImage.createAndSaveBackground(Environment.CurrentDirectory + cmdParsing.backgroundImageLocation, cmdParsing.backgroundColor);

                var resultSet = osuReplace.replaceImages(beatmapDirectory, Environment.CurrentDirectory + cmdParsing.backgroundImageLocation);
                Console.WriteLine(Environment.NewLine + "===================");
                Console.WriteLine("Finished");
                Console.WriteLine("===================");
                Console.WriteLine("[replaced={0}]\n[failed/skipped={1}]", resultSet.FindAll(x => x == true).Count, resultSet.FindAll(x => x == false).Count);
            }

            else
            {
                Console.WriteLine("Restoring beatmap images...");
                var resultSet = osuReplace.revertImages(beatmapDirectory);
                Console.WriteLine("===================");
                Console.WriteLine("Finished");
                Console.WriteLine("===================");
                Console.WriteLine("[restored={0}]\n[failed/skipped={1}]", resultSet.FindAll(x => x == true).Count, resultSet.FindAll(x => x == false).Count);
            }

            Console.WriteLine(Environment.NewLine + "Press any key to exit.");
            Console.ReadKey();
        }
    }
}
