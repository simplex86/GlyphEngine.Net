using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace GlyphEngine
{
    /// <summary>
    /// 
    /// </summary>
    internal class Color
    {
        /// <summary> 
        /// Red component. 
        /// </summary>
        public uint R { get; set; }
        /// <summary> 
        /// Green component. 
        /// </summary>
        public uint G { get; set; }
        /// <summary> 
        /// Blue component. 
        /// </summary>
        public uint B { get; set; }

        /// <summary> 
        /// Creates a new Color from rgb. 
        /// </summary>
        public Color(int r, int g, int b)
        {
            this.R = (uint)r;
            this.G = (uint)g;
            this.B = (uint)b;
        }
    }

    /// <summary>
    /// 文件访问权限
    /// </summary>
    internal enum EFileAccessMode : uint
    {
        /// <summary>
        /// 所有可能的访问权限
        /// </summary>
        All = 0x10000000,
        /// <summary>
        /// 执行访问权限
        /// </summary>
        Execute = 0x20000000,
        /// <summary>
        /// 写入访问权限
        /// </summary>
        Write = 0x40000000,
        /// <summary>
        /// 读取访问权限
        /// </summary>
        Read = 0x80000000,
    }

    /// <summary>
    /// 文件共享模式
    /// </summary>
    internal enum EFileShareMode
    {
        /// <summary>
        /// 如果其他进程请求删除、读取或写入访问权限，则阻止其他进程打开文件或设备
        /// </summary>
        None = 0x00000000,
        /// <summary>
        /// 允许对文件或设备执行后续打开操作以请求读取访问权限。否则，如果进程请求读取访问权限，则其他进程无法打开文件或设备。
        /// 如果未指定此标志，但已打开文件或设备进行读取访问，则函数将失败。
        /// </summary>
        Read = 0x00000001,
        /// <summary>
        /// 允许对文件或设备执行后续打开操作以请求写入访问权限。否则，如果进程请求写入访问权限，则其他进程无法打开文件或设备。
        /// 如果未指定此标志，但已打开文件或设备进行写入访问或具有写入访问权限的文件映射，则函数将失败。
        /// </summary>
        Write = 0x00000002,
        /// <summary>
        /// 启用对文件或设备上的后续打开操作以请求删除访问权限。否则，如果进程请求删除访问权限，则无法打开文件或设备。
        /// 如果未指定此标志，但文件或设备已打开以删除访问权限，则函数将失败。
        /// </summary>
        Delete = 0x00000004,
    }

    /// <summary>
    /// 
    /// </summary>
    internal class WindowsNativeAPI
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct CharInfo
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ColorRef
        {
            internal uint dw_color;

            internal ColorRef(Color color)
            {
                dw_color = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
            }

            internal ColorRef(uint r, uint g, uint b)
            {
                dw_color = r + (g << 8) + (b << 16);
            }

            internal Color GetColor()
            {
                return new Color((int)(0x000000FFU & dw_color),
                                 (int)(0x0000FF00U & dw_color) >> 8, 
                                 (int)(0x00FF0000U & dw_color) >> 16);
            }

            internal void SetColor(Color color)
            {
                dw_color = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_SCREEN_BUFFER_INFO_EX
        {
            public int cbSize;
            public Coord dwSize;
            public Coord dwCursorPosition;
            public short wAttributes;
            public SmallRect srWindow;
            public Coord dwMaximumWindowSize;

            public ushort wPopupAttributes;
            public bool bFullscreenSupported;

            internal ColorRef black;
            internal ColorRef darkBlue;
            internal ColorRef darkGreen;
            internal ColorRef darkCyan;
            internal ColorRef darkRed;
            internal ColorRef darkMagenta;
            internal ColorRef darkYellow;
            internal ColorRef gray;
            internal ColorRef darkGray;
            internal ColorRef blue;
            internal ColorRef green;
            internal ColorRef cyan;
            internal ColorRef red;
            internal ColorRef magenta;
            internal ColorRef yellow;
            internal ColorRef white;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CONSOLE_FONT_INFO_EX
        {
            public uint cbSize;
            public uint nFont;
            public Coord dwFontSize;
            public int FontFamily;
            public int FontWeight;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] // Edit sizeconst if the font name is too big
            public string FaceName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern short GetAsyncKeyState(Int32 vKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetCursorPos(out POINT vKey);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="x"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="wFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWndFrom"></param>
        /// <param name="hWndTo"></param>
        /// <param name="rect"></param>
        /// <param name="cPoints"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref Rect rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nStdHandle"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileAccess"></param>
        /// <param name="fileShare"></param>
        /// <param name="securityAttributes"></param>
        /// <param name="creationDisposition"></param>
        /// <param name="flags"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="dwBufferSize"></param>
        /// <param name="dwBufferCoord"></param>
        /// <param name="lpWriteRegion"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutputW(
            SafeFileHandle hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="csbe"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConsoleOutput"></param>
        /// <param name="csbe"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleScreenBufferInfoEx(IntPtr ConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConsoleOutput"></param>
        /// <param name="MaximumWindow"></param>
        /// <param name="ConsoleCurrentFontEx"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 SetCurrentConsoleFontEx(IntPtr ConsoleOutput, bool MaximumWindow, ref CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hConsoleHandle"></param>
        /// <param name="dwMode"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
    }
}
