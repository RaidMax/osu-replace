using System;
using System.IO;

namespace osu_replace
{
    class Run
    {
        static void Main(string[] args)
        {
            string replacementImagePath = String.Empty;
            string beatmapDirectory = "Songs";

            if (!Directory.Exists(beatmapDirectory))
            {
                Console.WriteLine("Please put me in your osu! game directory.");
                Console.ReadKey();
                return;
            }

            if (args.Length > 0 && (args[0].Contains(".jpg") || args[0].Contains(".png")))
            {
                var resultSet = osuReplace.replaceImages(beatmapDirectory, args[0]);
                Console.WriteLine(String.Format("===Finished: [replaced={0}] [failed/skipped={1}]===", resultSet.FindAll(x => x == true).Count, resultSet.FindAll(x => x == false).Count));
            }

            else
            {
                Console.WriteLine("Assuming you want to restore beatmaps images...");
                var resultSet = osuReplace.revertImages(beatmapDirectory);
                Console.WriteLine(String.Format("=====================================================\nFinished: [restored={0}] [failed/skipped={1}]", resultSet.FindAll(x => x == true).Count, resultSet.FindAll(x => x == false).Count));
            }

            Console.ReadKey();
        }
    }
}
