using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace osu_replace
{
    class SymbolicLink
    {
        [DllImport("kernel32.dll")]
        private static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymLinkType dwFlags);

        private enum SymLinkType
        {
            File = 0,
            Directory = 1
        }

        public static bool makeLink(string symLink, string fileName)
        {
            return CreateSymbolicLink(symLink, fileName, SymLinkType.File);
        }
    }
}
