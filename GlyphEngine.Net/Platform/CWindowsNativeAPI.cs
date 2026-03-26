using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace GlyphEngine
{
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
    internal class CWindowsNativeAPI
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        public struct CharInfo
        {
            [FieldOffset(0)] public char UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
            [FieldOffset(2)] public short Attributes;
        }

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
        /// <param name="fileName"></param>
        /// <param name="fileAccess"></param>
        /// <param name="fileShare"></param>
        /// <param name="securityAttributes"></param>
        /// <param name="creationDisposition"></param>
        /// <param name="flags"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(string fileName,
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
        public static extern bool WriteConsoleOutputW(SafeFileHandle hConsoleOutput,
                                                      CharInfo[] lpBuffer,
                                                      Coord dwBufferSize,
                                                      Coord dwBufferCoord,
                                                  ref SmallRect lpWriteRegion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="wAttributes"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleTextAttribute(IntPtr hConsoleOutput, ushort wAttributes);
    }
}
