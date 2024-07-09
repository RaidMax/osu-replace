using System;
using System.Runtime.InteropServices;

namespace OsuReplace.Code;

public static partial class WindowsCallbacks
{
    private const int ButtonDown = 0xA1;
    private const int Caption = 0x2;

    [LibraryImport("User32.dll", SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)] 
    private static partial bool ReleaseCapture();

    [LibraryImport("User32.dll", SetLastError = true, EntryPoint = "SendMessageW")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static partial int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    public static void SendDragMessage(IntPtr handle)
    {
        ReleaseCapture();
        SendMessage(handle, ButtonDown, Caption, 0);
    }
}
