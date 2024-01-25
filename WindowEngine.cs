using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TalkingFlowers
{
    public static class WindowEngine
    {
        public static List<WindowInfo> WindowInfoList = new List<WindowInfo>();
        public static IntPtr TalkingFlowerWindow;
        public static void Update()
        {
            WindowInfoList.Clear();

            EnumWindows((IntPtr hWnd, IntPtr lParam) =>
            {
                if (!IsWindowVisible(hWnd))
                {
                    return true;
                }

                StringBuilder titleBuilder = new StringBuilder(256);
                GetWindowText(hWnd, titleBuilder, titleBuilder.Capacity);
                string title = titleBuilder.ToString();

                if (title is null || title is "")
                {
                    return true;
                }

                WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                placement.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                GetWindowPlacement(hWnd, ref placement);

                if (!(placement.showCmd is 1))
                {
                    return true;
                }

                if (title is "Settings" || title is "Microsoft Text Input Application" || title is "NVIDIA GeForce Overlay" || title is "Program Manager")
                {
                    return true;
                }

                if (hWnd == TalkingFlower.WindowHandle)
                {
                    return true;
                }

                RECT clientRect;
                GetClientRect(hWnd, out clientRect);

                POINT upperLeft = new POINT() { X = clientRect.Left, Y = clientRect.Top };
                POINT lowerRight = new POINT() { X = clientRect.Right, Y = clientRect.Bottom };

                ClientToScreen(hWnd, ref upperLeft);
                ClientToScreen(hWnd, ref lowerRight);

                WindowInfo windowInfo = new WindowInfo();

                windowInfo.PositionX = upperLeft.X;
                windowInfo.PositionY = upperLeft.Y;
                windowInfo.Width = lowerRight.X - upperLeft.X;
                windowInfo.Height = lowerRight.Y - upperLeft.Y;

                WindowInfoList.Add(windowInfo);

                return true;
            }, IntPtr.Zero);
        }
        public sealed class WindowInfo
        {
            public int PositionX;
            public int PositionY;
            public int Width;
            public int Height;
        }
        #region PInvoke Bindings
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.I4)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        }

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        #endregion
    }
}
