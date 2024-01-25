using System;
using System.Runtime.InteropServices;

namespace sELedit.configs
{
    public class ApiHelper
    {
        public const Int32 WS_EX_LAYERED = 524288;
        public const int WM_NCHITTEST = 132;
        public const int HTCLIENT = 1;
        public const int HTCAPTION = 2;
        public const Int32 ULW_ALPHA = 2;
        public const byte AC_SRC_OVER = 0;
        public const byte AC_SRC_ALPHA = 1;


        [StructLayout(LayoutKind.Sequential)]
        public struct ApiPoint
        {
            public int X;
            public int Y;
            public ApiPoint(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public ApiPoint(System.Drawing.Point pt)
            {
                this.X = pt.X;
                this.Y = pt.Y;
            }
        }

        public enum BoolEnum
        {
            False = 0,
            True = 1
        }

        public enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ApiSize
        {
            public Int32 cx;
            public Int32 cy;
            public ApiSize(Int32 cx, Int32 cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Margins
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        public enum GWL : int
        {
            ExStyle = -20
        }

        public enum WS_EX : int
        {
            Transparent = 32,
            Layered = 524288
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern BoolEnum UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref ApiPoint pptDst, ref ApiSize psize, IntPtr hdcSrc, ref ApiPoint pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern BoolEnum DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern BoolEnum DeleteObject(IntPtr hObject);


    }
}
