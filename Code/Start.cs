using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Serilog;
using Serilog.Core;

namespace OsuReplace.Code;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal abstract class Start
{
    public static readonly Logger Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("OsuReplace.log").CreateLogger();

    [STAThread]
    public static void Main()
    {
        Application.Run(new UI.MainWindow());
    }
}
