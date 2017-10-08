#if DEPRECATED
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsuReplace
{
    class cmdParsing
    {
        public static bool restoreImages { get; private set; }
        public static System.Drawing.Color backgroundColor { get; private set; }
        public static string backgroundImageLocation { get; private set; }
        public static string beatmapDirectory { get; private set; }

        public static bool Load(string[] args)
        {
            try
            {
                backgroundImageLocation = "BG.jpg";
                // this allows dragging the file into the program
                if (args.Length > 0 && (args[0].Contains(".png") || args[0].Contains(".jpg")))
                    backgroundImageLocation = args[0];

                var options = new NDesk.Options.OptionSet()
                {
                    {  "r|restore=", "restore all beatmap backgrounds", r => { if (r != null) restoreImages = Boolean.Parse(r); } },
                    {  "c|color=", "color of beatmap backgrounds", c => { if ( c != null ) backgroundColor = osuImage.colorFromString(c); } },
                    {  "d|directory=", "relative directory of beatmap folder ( if different than `Songs` )", d => { if ( d != null ) beatmapDirectory = d; } },
                };

                options.Parse(args);
                return true;
             }

            catch (Exception)
            {
                return false;
            }
        }
    }
}
#endif
