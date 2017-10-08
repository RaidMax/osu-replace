using System;
using System.Windows.Forms;

namespace OsuReplace.Code
{
    class Start
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new UI.MainWindow());
        }
    }
}
