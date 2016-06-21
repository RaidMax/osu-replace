using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace osu_replace
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
                // this allows dragging the file into the program
                if (args.Length > 0)
                    backgroundImageLocation = args[0];

                var options = new NDesk.Options.OptionSet()
                {
                    {  "r|restore=", "restore all beatmap backgrounds", x => { if (x != null) restoreImages = Boolean.Parse(x); } },
                    {  "c|color=", "color of beatmap backgrounds", c => { if ( c != null ) { backgroundColor = osuImage.colorFromString(c); backgroundImageLocation = "BG.jpg";  }} },
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
